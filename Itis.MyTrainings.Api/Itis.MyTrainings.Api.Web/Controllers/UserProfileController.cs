﻿using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;
using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PutUserProfile;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.PostUserProfile;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.PutUserProfile;
using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер для профиля пользователя
/// </summary>
public class UserProfileController : BaseController
{
    /// <summary>
    /// Получить профиль пользователя по идентификатору
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet]
    public async Task<GetUserProfileByIdResponse> GetProfileById(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken) 
        => await mediator.Send(
            new GetUserProfileByIdQuery(CurrentUserId), 
            cancellationToken);

    /// <summary>
    /// Создать профиль пользователя
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpPost]
    public async Task<PostUserProfileResponse> PostUserProfile(
        [FromServices] IMediator mediator,
        [FromBody] PostUserProfileRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(
            new PostUserProfileCommand(CurrentUserId)
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Height = request.Height,
                Weight = request.Weight,
            }, cancellationToken);

    /// <summary>
    /// Обновить профиль пользователя
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Идентификатор обновленной сущности</returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpPut]
    public async Task<PutUserProfileResponse> PutUserProfile(
        [FromServices] IMediator mediator,
        [FromBody] PutUserProfileRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PutUserProfileCommand(CurrentUserId)
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            Height = request.Height,
            Weight = request.Weight,
        }, cancellationToken);
}