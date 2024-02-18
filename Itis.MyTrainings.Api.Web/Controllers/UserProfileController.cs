using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.GetUserProfileById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер для профиля пользователя
/// </summary>
public class UserProfileController
{
    /// <summary>
    /// Получить профиль пользователя по идентификатор
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [HttpGet("GetProfileById/{userId}")]
    public async Task<GetUserProfileByIdResponse> GetProfileById(
        [FromServices] IMediator mediator,
        [FromRoute] Guid userId,
        CancellationToken cancellationToken) =>
        await mediator.Send(
            new GetUserProfileByIdQuery(userId), 
            cancellationToken);

    #region 

    /// <summary>
    /// Пасхалочка
    /// </summary>
    /// <returns></returns>
    [HttpGet("chipi")]
    public IActionResult Chipi()
    {
        return new ContentResult()
        {
            Content = $"<!DOCTYPE html>\n<html>\n<head>\n   " +
                      $"</head>\n<body>\n    <img style=\"width: 100%\" " +
                      $"src=\"https://media1.tenor.com/m/s7Tf_aL-Di0AAAAC/chipi-chipi-chapa-chapa.gif\" alt=\"Гифка\">\n</body>\n</html>",
            ContentType = "text/html",
            StatusCode = 200,
        };
    }

    #endregion
}