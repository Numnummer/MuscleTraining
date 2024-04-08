using Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExerciseById;
using Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExercises;
using Itis.MyTrainings.Api.Contracts.Requests.Exercise.PostExercise;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Requests.Exercise.GetExerciseById;
using Itis.MyTrainings.Api.Core.Requests.Exercise.GetExercises;
using Itis.MyTrainings.Api.Core.Requests.Exercise.PostExercise;
using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
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
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpPost]
    public async Task<PostExerciseResponse> PostExerciseAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostExerciseRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostExerciseCommand(CurrentUserId)
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
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet]
    public async Task<GetExercisesResponse> GetExercisesAsync(
        [FromServices] IMediator mediator,
        [FromQuery] GetExercisesRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetExercisesQuery(CurrentUserId), cancellationToken);

    /// <summary>
    /// Получить упражнение по Id
    /// </summary>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet]
    public async Task<GetExerciseByIdResponse> GetExerciseByIdAsync(
        [FromServices] IMediator mediator,
        [FromQuery] Guid exerciseId,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetExerciseByIdQuery(CurrentUserId, exerciseId), cancellationToken);
}