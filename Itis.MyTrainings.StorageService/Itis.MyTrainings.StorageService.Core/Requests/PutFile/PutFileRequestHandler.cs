using Amazon.S3;
using Amazon.S3.Model;
using Itis.MyTrainings.StorageService.Core.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace Itis.MyTrainings.StorageService.Core.Requests.PutFile;

public class PutFileRequestHandler(IAmazonS3 _s3Client,
    IOptionsMonitor<S3Options> options) : IRequestHandler<PutFileRequest>
{
    public async Task Handle(PutFileRequest request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.Length == 0)
                throw new FileNotFoundException();
    
        var keyName = Path.GetFileName(request.File.FileName);
        var fileSize = request.File.Length;
        var partSize = 5 * (1024 * 1024); // 5 MB

        await _s3Client.EnsureBucketExistsAsync(options.CurrentValue.BucketName);
        
        // Инициализация multipart загрузки
        var initiateRequest = new InitiateMultipartUploadRequest
        {
            BucketName = options.CurrentValue.BucketName,
            Key = keyName
        };

        var initResponse = await _s3Client.InitiateMultipartUploadAsync(initiateRequest);
        var partETags = new List<PartETag>();
        try
        {
            // Разделение файла на части и загрузка
            long position = 0;
            for (int i = 1; position < fileSize; i++)
            {
                partSize = (int)Math.Min(partSize, fileSize - position);
                using (var stream = new MemoryStream())
                {
                    request.File.OpenReadStream().Position = position;
                    await request.File.OpenReadStream().CopyToAsync(stream, partSize);
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

                    var uploadPartResponse = await _s3Client.UploadPartAsync(uploadRequest);
                    partETags.Add(new PartETag(i,uploadPartResponse.ETag));

                    position += partSize; // Увеличиваем позицию для следующей части
                }
            }

            // Завершение multipart загрузки
            var completeRequest = new CompleteMultipartUploadRequest
            {
                BucketName = options.CurrentValue.BucketName,
                Key = keyName,
                UploadId = initResponse.UploadId,
                PartETags = partETags
            };

            await _s3Client.CompleteMultipartUploadAsync(completeRequest);
        }
        catch
        {
            // В случае ошибки, отменяем загрузку
            await _s3Client.AbortMultipartUploadAsync(new AbortMultipartUploadRequest
            {
                BucketName = options.CurrentValue.BucketName,
                Key = keyName,
                UploadId = initResponse.UploadId
            });

            throw;
        }
    }
}