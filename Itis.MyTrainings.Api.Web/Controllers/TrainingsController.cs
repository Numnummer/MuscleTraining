using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainingById;
using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainings;
using Itis.MyTrainings.Api.Contracts.Requests.Training.PostTraining;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Requests.Training.AddExercisesInTraining;
using Itis.MyTrainings.Api.Core.Requests.Training.DeleteTraining;
using Itis.MyTrainings.Api.Core.Requests.Training.GetTrainingById;
using Itis.MyTrainings.Api.Core.Requests.Training.GetTrainings;
using Itis.MyTrainings.Api.Core.Requests.Training.PostTraining;
using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер сущности "Тренировки"
/// </summary>
public class TrainingsController: BaseController
{
    /// <summary>
    /// Создание тренировки
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Идентификатор тренировки</returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpPost]
    public async Task<PostTrainingResponse> PostTrainingAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostTrainingRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostTrainingCommand(CurrentUserId)
        {
            Name = request.Name,
            TrainingDate = request.TrainingDate,
            ExerciseIds = request.ExerciseIds,
        }, cancellationToken);

    /// <summary>
    /// Получить все тренировки
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet]
    public async Task<GetTrainingsResponse> GetTrainingsAsync(
        [FromServices] IMediator mediator,
        [FromQuery] GetTrainingsRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetTrainingsQuery(CurrentUserId), cancellationToken);
    
    /// <summary>
    /// Получить тренировку по Id
    /// </summary>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("{trainingId}")]
    public async Task<GetTrainingByIdResponse> GetTrainingByIdAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid trainingId,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetTrainingByIdQuery(CurrentUserId, trainingId), cancellationToken);
    
    /// <summary>
    /// Удалить тренировку по Id
    /// </summary>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpDelete("{trainingId}")]
    public async Task DeleteTrainingAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid trainingId,
        CancellationToken cancellationToken)
        => await mediator.Send(new DeleteTrainingCommand(trainingId), cancellationToken);

    /// <summary>
    /// Добавить упражнения в тренировку
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="trainingId"></param>
    /// <param name="exerciseIds"></param>
    /// <param name="cancellationToken"></param>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpPost("{trainingId}/addExercises")]
    public async Task AddExercisesInTraining(
        [FromServices] IMediator mediator,
        [FromRoute] Guid trainingId,
        [FromBody] List<Guid> exerciseIds,
        CancellationToken cancellationToken)
        => await mediator.Send(new AddExercisesInTrainingCommand(trainingId, exerciseIds), cancellationToken);
}