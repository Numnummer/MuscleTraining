using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.ChatHistoryService.Core.Request.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.ChatHistoryService.Core.Request.SupportChat.LoadUnicastChatHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.ChatHistoryService.Web.Controllers;

[Route("api/[controller]")]
public class ChatHistoryController : ControllerBase
{
    [HttpGet("multicast/{group}")]
    public async Task<LoadChatHistoryResponse[]> LoadMulticastChatHistoryAsync(
        [FromServices] IMediator mediator, string group) =>
        await mediator.Send(new LoadMulticastChatHistoryQuery(group));
    
    [HttpGet("unicast/{firstEmail}/{secondEmail}")]
    public async Task<LoadChatHistoryResponse[]> LoadUnicastChatHistoryAsync(
        [FromServices] IMediator mediator, string firstEmail, string secondEmail) =>
        await mediator.Send(new LoadUnicastChatHistoryQuery(firstEmail, secondEmail));
}