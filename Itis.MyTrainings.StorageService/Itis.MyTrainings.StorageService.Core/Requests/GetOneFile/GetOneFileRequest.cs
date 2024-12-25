using Amazon.Runtime.Internal;
using MediatR;
using StorageS3Shared;

namespace Itis.MyTrainings.StorageService.Core.Requests.GetOneFile;

public class GetOneFileRequest(string fileName) : IRequest<FileModel>
{
    public string FileName { get; set; } = fileName;
}