namespace Itis.MyTrainings.Api.Contracts.Requests.Training.PostTraining;

/// <summary>
/// Ответ на <see cref="PostTrainingRequest"/>
/// </summary>
public class PostTrainingResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="trainingId">Идентификатор созданной тренировки</param>
    public PostTrainingResponse(Guid trainingId)
    {
        TrainingId = trainingId;
    }
    
    /// <summary>
    /// Идентификатор созданной тренировки
    /// </summary>
    public Guid TrainingId { get; set; }
}