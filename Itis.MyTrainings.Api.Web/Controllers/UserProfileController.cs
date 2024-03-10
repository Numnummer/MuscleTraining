using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.PostUserProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal.TypeHandlers.FullTextSearchHandlers;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер для профиля пользователя
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserProfileController
{
    /// <summary>
    /// Получить профиль пользователя по идентификатору
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [HttpGet("profileByUserId")]
    public async Task<GetUserProfileByIdResponse> GetProfileById(
        [FromServices] IMediator mediator,
        [FromQuery] Guid userId,
        CancellationToken cancellationToken) 
        => await mediator.Send(
            new GetUserProfileByIdQuery(userId), 
            cancellationToken);

    /// <summary>
    /// Создать профиль пользователя
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [HttpPost("profile")]
    public async Task<PostUserProfileResponse> PostUserProfile(
        [FromServices] IMediator mediator,
        [FromQuery] Guid userId,
        [FromBody] PostUserProfileRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(
            new PostUserProfileCommand(userId)
            {
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Height = request.Height,
                Weight = request.Weight,
            }, cancellationToken);
}