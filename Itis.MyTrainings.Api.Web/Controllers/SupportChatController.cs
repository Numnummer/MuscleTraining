using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Requests.SupportChat;
using Itis.MyTrainings.Api.Core.Requests.SupportChat.LoadUnicastChatHistory;
using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер для получения истории сообщений из чата поддержки
/// </summary>
public class SupportChatController : BaseController
{
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("multicast/{group}")]
    public async Task<LoadChatHistoryResponse[]> LoadMulticastChatHistoryAsync(
        [FromServices] IMediator mediator, string group) =>
        await mediator.Send(new LoadMulticastChatHistoryQuery(group));
    
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("unicast/{email}")]
    public async Task<LoadChatHistoryResponse[]> LoadUnicastChatHistoryAsync(
        [FromServices] IMediator mediator, string email) =>
        await mediator.Send(new LoadUnicastChatHistoryQuery(email));

}