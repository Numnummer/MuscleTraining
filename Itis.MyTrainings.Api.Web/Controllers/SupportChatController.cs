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
    [HttpGet]
    public async Task<LoadChatHistoryResponse[]> LoadMulticastChatHistoryAsync(
        [FromServices] IMediator mediator, [FromBody] LoadMulticastChatHistoryQuery request) =>
        await mediator.Send(request);
    
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet]
    public async Task<LoadChatHistoryResponse[]> LoadUnicastChatHistoryAsync(
        [FromServices] IMediator mediator, [FromBody] LoadUnicastChatHistoryQuery request) =>
        await mediator.Send(request);

}