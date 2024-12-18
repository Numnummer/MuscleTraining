using Amazon.S3;
using Amazon.S3.Model;
using Itis.MyTrainings.StorageService.Core.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using StorageS3Shared;

namespace Itis.MyTrainings.StorageService.Core.Requests.GetOneFile;

public class GetOneFileRequestHandler(IAmazonS3 s3Client,
    IOptionsMonitor<S3Options> options) : IRequestHandler<GetOneFileRequest, FileModel>
{
    public async Task<FileModel> Handle(GetOneFileRequest request, CancellationToken cancellationToken)
    {
        var objectResponse = await s3Client.GetObjectAsync(new GetObjectRequest
        {
            BucketName = options.CurrentValue.BucketName,
            Key = request.FileName
        });
        using var memoryStream = new MemoryStream();
        await objectResponse.ResponseStream.CopyToAsync(memoryStream);
        return new FileModel(request.FileName, memoryStream.ToArray());
    }
}