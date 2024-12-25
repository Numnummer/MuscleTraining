using Amazon.S3;
using Itis.MyTrainings.StorageService.Core.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace Itis.MyTrainings.StorageService.Core.Requests.DeleteFile;

public class DeleteFileRequestHandler(IAmazonS3 s3Client,
    IOptionsMonitor<S3Options> options) : IRequestHandler<DeleteFileRequest>
{
    public async Task Handle(DeleteFileRequest request, CancellationToken cancellationToken)
    {
        await s3Client.DeletesAsync(options.CurrentValue.BucketName, 
            request.FilesPath, null, cancellationToken);
    }
}