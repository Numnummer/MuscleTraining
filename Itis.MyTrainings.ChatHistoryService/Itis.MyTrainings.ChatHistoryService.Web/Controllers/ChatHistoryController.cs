using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.S3Communication;
using Itis.MyTrainings.ChatHistoryService.Core.Request.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.ChatHistoryService.Core.Request.SupportChat.LoadUnicastChatHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StorageS3Shared;

namespace Itis.MyTrainings.ChatHistoryService.Web.Controllers;

[Route("api/[controller]")]
public class ChatHistoryController : ControllerBase
{
    private readonly IOptionsMonitor<StorageServiceOptions> _storageOptions;

    public ChatHistoryController(IOptionsMonitor<StorageServiceOptions> storageOptions)
    {
        _storageOptions = storageOptions;
    }

    [HttpGet("multicast/{group}")]
    public async Task<LoadChatHistoryResponse[]> LoadMulticastChatHistoryAsync(
        [FromServices] IMediator mediator, string group) =>
        await mediator.Send(new LoadMulticastChatHistoryQuery(group));
    
    [HttpGet("unicast/{firstEmail}/{secondEmail}")]
    public async Task<LoadChatHistoryResponse[]> LoadUnicastChatHistoryAsync(
        [FromServices] IMediator mediator, string firstEmail, string secondEmail) =>
        await mediator.Send(new LoadUnicastChatHistoryQuery(firstEmail, secondEmail));

    [HttpGet("getOneFile/{fileName}")]
    public async Task<FileModel> GetOneFileAsync(string fileName)
    {
        var httpClient = new HttpClient();
        var url = $"{_storageOptions.CurrentValue.Url}/getOneFile/{fileName}";
        return await httpClient.GetFromJsonAsync<FileModel>(url);
    }
}