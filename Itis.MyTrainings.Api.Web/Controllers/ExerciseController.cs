﻿using Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExercises;
using Itis.MyTrainings.Api.Contracts.Requests.Exercise.PostExercise;
using Itis.MyTrainings.Api.Core.Requests.Exercise.GetExercises;
using Itis.MyTrainings.Api.Core.Requests.Exercise.PostExercise;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер упражнений
/// </summary>
public class ExerciseController: BaseController
{
    /// <summary>
    /// Создать Упражнение
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("{userId}")]
    public async Task<PostExerciseResponse> PostExerciseAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid userId,
        [FromBody] PostExerciseRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostExerciseCommand(userId)
        {
            Name = request.Name,
            Description = request.Description,
            Approaches = request.Approaches,
            Repetitions = request.Repetitions,
            ImplementationProgress = request.ImplementationProgress,
            ExplanationVideo = request.ExplanationVideo,
        }, cancellationToken);

    /// <summary>
    /// Получить упражнения
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<GetExercisesResponse> GetExercisesAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid userId,
        [FromQuery] GetExercisesRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetExercisesQuery(userId), cancellationToken);
}