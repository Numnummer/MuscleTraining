using MediatR;
using File = Itis.MyTrainings.StorageService.Core.Entities.File;

namespace Itis.MyTrainings.StorageService.Core.Requests.GetFile;

public class GetFileRequest(string fileName) : IRequest<File>
{
    public string FileName { get; set; } = fileName;
}