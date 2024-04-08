using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainingById;
using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainings;
using Itis.MyTrainings.Api.Contracts.Requests.Training.PostTraining;
using Itis.MyTrainings.Api.Core.Constants;
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
public class TrainingController: BaseController
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
    [HttpGet]
    public async Task<GetTrainingByIdResponse> GetTrainingByIdAsync(
        [FromServices] IMediator mediator,
        [FromQuery] Guid trainingId,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetTrainingByIdQuery(CurrentUserId, trainingId), cancellationToken);
}