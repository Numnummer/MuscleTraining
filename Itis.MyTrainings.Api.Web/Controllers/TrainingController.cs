﻿using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainings;
using Itis.MyTrainings.Api.Contracts.Requests.Training.PostTraining;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Requests.Training.GetTrainings;
using Itis.MyTrainings.Api.Core.Requests.Training.PostTraining;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер сущности "Тренировки"
/// </summary>
public class TrainingController: BaseController
{
    /// <summary>
    /// Создание тренировки
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Идентификатор тренировки</returns>
    [Authorize(Policy = PolicyConstants.IsDefaultUser)]
    [HttpPost("{userId}")]
    public async Task<PostTrainingResponse> PostTrainingAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid userId,
        [FromBody] PostTrainingRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostTrainingCommand(userId)
        {
            Name = request.Name,
            TrainingDate = request.TrainingDate,
            ExerciseIds = request.ExerciseIds,
        }, cancellationToken);

    /// <summary>
    /// Получить все тренировки
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Authorize(Policy = PolicyConstants.IsDefaultUser)]
    [HttpGet("{userId}")]
    public async Task<GetTrainingsResponse> GetTrainingsAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid userId,
        [FromQuery] GetTrainingsRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetTrainingsQuery(userId), cancellationToken);
}