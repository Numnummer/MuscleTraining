using Amazon.S3;
using Amazon.S3.Model;
using Itis.MyTrainings.StorageService.Core.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using StorageS3Shared;

namespace Itis.MyTrainings.StorageService.Core.Requests.PutFile;

public class PutFileRequestHandler : IRequestHandler<PutFileRequest>
{
    private readonly string _loadCountKey = "loadCount";
    private readonly IAmazonS3 _s3Client;
    private readonly IAmazonS3 _s3TempClient;
    private readonly IOptionsMonitor<S3Options> options;
    private readonly IConnectionMultiplexer cacheConnection;

    public PutFileRequestHandler(IOptionsMonitor<S3Options> options, IConnectionMultiplexer cacheConnection,
        IServiceProvider serviceProvider)
    {
        this.options = options;
        this.cacheConnection = cacheConnection;
        _s3Client = serviceProvider.GetServices<IAmazonS3>()
            .First(client => client.Config.ServiceURL == "http://minio:9000/"); // Adjust this logic if necessary

        _s3TempClient = serviceProvider.GetServices<IAmazonS3>()
            .First(client => client.Config.ServiceURL == "http://minio:9001/");
    }
    public async Task Handle(PutFileRequest request, CancellationToken cancellationToken)
    {
        foreach (var file in request.Files)
        {
            try
            {
                await LoadMetadata(file.FileName, "", cancellationToken);
                await LoadFile(_s3TempClient, file, cancellationToken);
                await LoadFile(_s3Client, file, cancellationToken);
            }
            catch (Exception e)
            {
                await DeleteMetadata(file.FileName, cancellationToken);
                await DeleteFile(_s3TempClient, file, cancellationToken);
                await DeleteFile(_s3Client, file, cancellationToken);
            }
            
        }
    }
    
    private async Task LoadMetadata(string fileName, string json, CancellationToken cancellationToken)
    {
        var db = cacheConnection.GetDatabase();
        if (await db.KeyExistsAsync(_loadCountKey))
        {
            var loadCount = await db.StringGetAsync(_loadCountKey);
            var count = int.Parse(loadCount.ToString());
            if (count>=2)
            {
                var server = cacheConnection.GetServer(cacheConnection.GetEndPoints().First());
                await server.FlushDatabaseAsync();
                return;
            }

            await db.StringSetAsync(fileName, json);
            await db.StringSetAsync(_loadCountKey, (count + 1).ToString());
            return;
        }
        await db.StringSetAsync(fileName, json);
        await db.StringSetAsync(_loadCountKey, "1");
    }
    
    private async Task DeleteMetadata(string fileName, CancellationToken cancellationToken)
    {
        var db = cacheConnection.GetDatabase();
        await db.KeyDeleteAsync(fileName);
        var loadCount = await db.StringGetAsync(_loadCountKey);
        var count = int.Parse(loadCount.ToString());
        if(count<=0) return;
        await db.StringSetAsync(_loadCountKey, (count-1).ToString());
    }

    private async Task DeleteFile(IAmazonS3 client, FileModel file, CancellationToken cancellationToken)
    {
        var keyName = Path.GetFileName(file.FileName);
        await client.DeletesAsync(options.CurrentValue.BucketName, 
            [keyName], null, cancellationToken);
    }

    private async Task LoadFile(IAmazonS3 client, FileModel file, CancellationToken cancellationToken)
    {
        if (file == null || file.FileContent.Length == 0)
                throw new FileNotFoundException();
    
        var keyName = Path.GetFileName(file.FileName);
        var fileSize = file.FileContent.Length;
        var partSize = 5 * (1024 * 1024); // 5 MB

        var buckets = await client.ListBucketsAsync();
        if(buckets.Buckets == null || !buckets.Buckets.Exists(b=>
               b.BucketName.Equals(options.CurrentValue.BucketName)))
            await client.PutBucketAsync(options.CurrentValue.BucketName, cancellationToken);
        
        // Инициализация multipart загрузки
        var initiateRequest = new InitiateMultipartUploadRequest
        {
            BucketName = options.CurrentValue.BucketName,
            Key = keyName
        };

        var initResponse = await client.InitiateMultipartUploadAsync(initiateRequest);
        var partETags = new List<PartETag>();
        try
        {
            // Разделение файла на части и загрузка
            int position = 0;
            for (int i = 1; position < fileSize; i++)
            {
                partSize = (int)Math.Min(partSize, fileSize - position);
                using var stream = new MemoryStream();
                await stream.WriteAsync(file.FileContent.AsMemory(
                    position, (int)partSize), cancellationToken);
                stream.Position = 0;

                var uploadRequest = new UploadPartRequest
                {
                    BucketName = options.CurrentValue.BucketName,
                    Key = keyName,
                    UploadId = initResponse.UploadId,
                    PartNumber = i,
                    PartSize = stream.Length,
                    InputStream = stream
                };

                var uploadPartResponse = await client.UploadPartAsync(uploadRequest);
                partETags.Add(new PartETag(i,uploadPartResponse.ETag));

                position += partSize; // Увеличиваем позицию для следующей части
            }

            // Завершение multipart загрузки
            var completeRequest = new CompleteMultipartUploadRequest
            {
                BucketName = options.CurrentValue.BucketName,
                Key = keyName,
                UploadId = initResponse.UploadId,
                PartETags = partETags
            };

            await client.CompleteMultipartUploadAsync(completeRequest);
        }
        catch
        {
            // В случае ошибки, отменяем загрузку
            await client.AbortMultipartUploadAsync(new AbortMultipartUploadRequest
            {
                BucketName = options.CurrentValue.BucketName,
                Key = keyName,
                UploadId = initResponse.UploadId
            });

            throw;
        }
    }
}