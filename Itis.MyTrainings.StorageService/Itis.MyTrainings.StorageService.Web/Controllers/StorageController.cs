using Itis.MyTrainings.StorageService.Core.Requests.DeleteFile;
using Itis.MyTrainings.StorageService.Core.Requests.GetFile;
using Itis.MyTrainings.StorageService.Core.Requests.PutFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using File = Itis.MyTrainings.StorageService.Core.Entities.File;

namespace Itis.MyTrainings.StorageService.Controllers;

[Route("storageApi/[controller]")]
public class StorageController(IMediator mediator) : ControllerBase
{
    [HttpPut("/putFile")]
    public async Task<IActionResult> PutAsync([FromForm] IFormFile file)
    {
        await mediator.Send(new PutFileRequest(file));
        return Ok();
    }

    [HttpGet("/getFile/{fileName}")]
    public async Task<File> GetFileAsync(string fileName)
    {
        return await mediator.Send(new GetFileRequest(fileName));
    }
    
    [HttpDelete("/deleteFile/{fileName}")]
    public async Task<IActionResult> DeleteFileAsync(string fileName)
    {
        await mediator.Send(new DeleteFileRequest(fileName));
        return Ok();
    }
}