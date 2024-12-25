using System.Collections;
using Amazon.S3;
using Amazon.S3.Model;
using Itis.MyTrainings.StorageService.Core.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using StorageS3Shared;

namespace Itis.MyTrainings.StorageService.Core.Requests.GetFile;

public class GetFileRequestHandler(IAmazonS3 s3Client,
    IOptionsMonitor<S3Options> options) : IRequestHandler<GetFileRequest, FileModel[][]>
{
    public async Task<FileModel[][]> Handle(GetFileRequest request, CancellationToken cancellationToken)
    {
        var fileModels = new HashSet<HashSet<FileModel>>();
        foreach (var messageFiles in request.FilesName)
        {
            var currentSet = new HashSet<FileModel>();
            foreach (var fileName in messageFiles)
            {
                var objectResponse = await s3Client.GetObjectAsync(new GetObjectRequest
                {
                    BucketName = options.CurrentValue.BucketName,
                    Key = fileName
                });
                using var memoryStream = new MemoryStream();
                await objectResponse.ResponseStream.CopyToAsync(memoryStream);
                currentSet.Add(new FileModel(fileName, memoryStream.ToArray()));
            }
            fileModels.Add(currentSet);
        }

        return fileModels.Select(f=>f.ToArray()).ToArray();
    }
}