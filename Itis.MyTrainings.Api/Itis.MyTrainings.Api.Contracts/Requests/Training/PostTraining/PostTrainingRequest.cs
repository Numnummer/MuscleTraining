namespace Itis.MyTrainings.Api.Contracts.Requests.Training.PostTraining;

/// <summary>
/// Запрос на создание Тренировки
/// </summary>
public class PostTrainingRequest
{
    /// <summary>
    /// Наименование тренировки
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Дата тренировки
    /// </summary>
    public DateTime TrainingDate { get; set; }

    /// <summary>
    /// Идентификаторы упражнений
    /// </summary>
    public List<Guid> ExerciseIds { get; set; } = default!;
}