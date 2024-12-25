using Itis.MyTrainings.StorageService.Core.Requests.DeleteFile;
using Itis.MyTrainings.StorageService.Core.Requests.GetFile;
using Itis.MyTrainings.StorageService.Core.Requests.GetOneFile;
using Itis.MyTrainings.StorageService.Core.Requests.PutFile;
using Itis.MyTrainings.StorageService.ModelBinders;
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
    public async Task<FileModel[][]> GetFileAsync()
    {
        var query=HttpContext.Request.Query;
        var result = new List<string[]>();
        foreach (var key in query.Keys)
        {
            if (!key.StartsWith("files[")) continue;
            var segments = key.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length != 3 || !int.TryParse(segments[1], out var i)) continue;
            while (result.Count <= i) result.Add(new List<string>().ToArray());
            result[i] = result[i].Concat(new string[] { query[key] }).ToArray();
        }
        return await mediator.Send(new GetFileRequest(result.ToArray()));
    }

    [HttpGet("/getOneFile/{fileName}")]
    public async Task<FileModel> GetOneFileAsync(string fileName)
    {
        return await mediator.Send(new GetOneFileRequest(fileName));
    }
    
    [HttpPost("/deleteFile")]
    public async Task<IActionResult> DeleteFileAsync([FromBody] string[] fileName)
    {
        await mediator.Send(new DeleteFileRequest(fileName));
        return Ok();
    }
}