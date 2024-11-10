namespace Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExerciseById;

/// <summary>
/// Ответ на получение упражнения
/// </summary>
public class GetExerciseByIdResponse
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Кол-во подходов
    /// </summary>
    public int? Approaches { get; set; }

    /// <summary>
    /// Кол-во повторений в подходе
    /// </summary>
    public int? Repetitions { get; set; }

    /// <summary>
    /// Ход выполнения
    /// </summary>
    public string? ImplementationProgress { get; set; }

    /// <summary>
    /// Ссылка на видео с объяснением
    /// </summary>
    public string? ExplanationVideo { get; set; }
}