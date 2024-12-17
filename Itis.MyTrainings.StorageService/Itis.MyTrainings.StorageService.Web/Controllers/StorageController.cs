using Itis.MyTrainings.StorageService.Core.Requests.DeleteFile;
using Itis.MyTrainings.StorageService.Core.Requests.GetFile;
using Itis.MyTrainings.StorageService.Core.Requests.PutFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorageS3Shared;

namespace Itis.MyTrainings.StorageService.Controllers;

[Route("storageApi/[controller]")]
public class StorageController(IMediator mediator) : ControllerBase
{
    [HttpPut("/putFile")]
    public async Task<IActionResult> PutAsync([FromBody] FileModel[] file)
    {
        await mediator.Send(new PutFileRequest(file));
        return Ok();
    }

    [HttpGet("/getFile")]
    public async Task<FileModel[][]> GetFileAsync([FromQuery] string[][] filesName)
    {
        return await mediator.Send(new GetFileRequest(filesName));
    }
    
    [HttpPost("/deleteFile")]
    public async Task<IActionResult> DeleteFileAsync([FromBody] string[] fileName)
    {
        await mediator.Send(new DeleteFileRequest(fileName));
        return Ok();
    }
}