using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http;
using IRequest = MediatR.IRequest;

namespace Itis.MyTrainings.StorageService.Core.Requests.PutFile;

public class PutFileRequest(IFormFile file) : IRequest
{
    public IFormFile File { get; set; } = file;
}