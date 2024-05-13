using Itis.MyTrainings.Api.Contracts.Requests.Message.GetMessages;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Requests.Message.GetMessages;
using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер сущности "Сообщения"
/// </summary>
public class MessageController : BaseController
{
    /// <summary>
    /// Получить все тренировки
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("messages")]
    public async Task<GetMessagesResponse> GetMessagesAsync(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetMessagesQuery(CurrentUserId), cancellationToken);
}