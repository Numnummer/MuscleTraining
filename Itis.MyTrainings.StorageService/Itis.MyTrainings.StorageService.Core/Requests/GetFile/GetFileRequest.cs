using MediatR;
using StorageS3Shared;

namespace Itis.MyTrainings.StorageService.Core.Requests.GetFile;

public class GetFileRequest(string fileName) : IRequest<FileModel>
{
    public string FileName { get; set; } = fileName;
}