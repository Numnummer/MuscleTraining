using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http;
using StorageS3Shared;
using IRequest = MediatR.IRequest;

namespace Itis.MyTrainings.StorageService.Core.Requests.PutFile;

public class PutFileRequest(FileModel[] file) : IRequest
{
    public FileModel[] Files { get; set; } = file;
}