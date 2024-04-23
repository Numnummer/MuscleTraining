using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainingById;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Training.GetTrainingById;

/// <summary>
/// Запрос на получение тренировки по Id
/// </summary>
public class GetTrainingByIdQuery : IRequest<GetTrainingByIdResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="trainingId">Идентификатор тренировки</param>
    public GetTrainingByIdQuery(Guid userId, Guid trainingId)
    {
        UserId = userId;
        TrainingId = trainingId;
    }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Идентификатор упражнения
    /// </summary>
    public Guid TrainingId { get; set; }
}