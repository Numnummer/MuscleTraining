using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Training.AddExercisesInTraining;

/// <summary>
/// Команда запроса на добавление упражнений в тренировку
/// </summary>
public class AddExercisesInTrainingCommand : IRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="trainingId">Идентификатор тренировки</param>
    /// <param name="exerciseIds">Идентификаторы упражнений</param>
    public AddExercisesInTrainingCommand(Guid trainingId, List<Guid> exerciseIds)
    {
        TrainingId = trainingId;
        ExerciseIds = exerciseIds;
    }

    /// <summary>
    /// Идентификатор тренировки
    /// </summary>
    public Guid TrainingId { get; set; }

    /// <summary>
    /// Идентификаторы упражнений
    /// </summary>
    public List<Guid> ExerciseIds { get; set; }
}