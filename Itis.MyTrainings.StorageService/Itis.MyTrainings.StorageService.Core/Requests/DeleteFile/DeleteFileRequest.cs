using MediatR;

namespace Itis.MyTrainings.StorageService.Core.Requests.DeleteFile;

public class DeleteFileRequest(string filePath) : IRequest
{
    public string FilePath { get; set; } = filePath;
}