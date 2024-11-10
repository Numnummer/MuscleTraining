namespace Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainingById;

/// <summary>
/// Ответ на получение тренировки по Id
/// </summary>
public class GetTrainingByIdResponse
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
    /// Упражнения
    /// </summary>
    public List<GetTrainingByIdExercise> Exercises { get; set; } = default!;
}