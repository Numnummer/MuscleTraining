namespace Itis.MyTrainings.Api.Contracts.Requests.Exercise.PostExercise;

/// <summary>
/// Ответ на <see cref="PostExerciseRequest"/>
/// </summary>
public class PostExerciseResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="exerciseId">Идентификатор сущности</param>
    public PostExerciseResponse(Guid exerciseId)
    {
        ExerciseId = exerciseId;
    }
    
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid ExerciseId { get; set; }
}