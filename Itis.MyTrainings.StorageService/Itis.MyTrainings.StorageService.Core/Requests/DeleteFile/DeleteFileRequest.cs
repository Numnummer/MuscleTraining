using MediatR;

namespace Itis.MyTrainings.StorageService.Core.Requests.DeleteFile;

public class DeleteFileRequest(string[] filePath) : IRequest
{
    public string[] FilesPath { get; set; } = filePath;
}