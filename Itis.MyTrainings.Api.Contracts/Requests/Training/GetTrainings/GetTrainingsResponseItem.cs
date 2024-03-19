namespace Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainings;

public class GetTrainingsResponseItem
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
    public List<GetTrainingsResponseExercise> Exercises { get; set; } = default!;
}