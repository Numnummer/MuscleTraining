using Amazon.S3;
using Itis.MyTrainings.StorageService.Core.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using File = Itis.MyTrainings.StorageService.Core.Entities.File;

namespace Itis.MyTrainings.StorageService.Core.Requests.GetFile;

public class GetFileRequestHandler(IAmazonS3 s3Client,
    IOptionsMonitor<S3Options> options) : IRequestHandler<GetFileRequest, File>
{
    public async Task<File> Handle(GetFileRequest request, CancellationToken cancellationToken)
    {
        var obj = await s3Client.GetObjectAsync(
            options.CurrentValue.BucketName, request.FileName, cancellationToken);
        var response = new File();
        response.FileName = obj.Key;
        await using (var stream = obj.ResponseStream)
        {
            using (var reader = new BinaryReader(stream))
            {
                response.FileContent = reader.ReadBytes((int)stream.Length);
            }
        }

        return response;
    }
}