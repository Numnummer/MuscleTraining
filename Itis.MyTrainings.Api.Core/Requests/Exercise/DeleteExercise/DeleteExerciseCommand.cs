using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Exercise.DeleteExercise;

/// <summary>
/// Запрос на удаление упражнения
/// </summary>
public class DeleteExerciseCommand: IRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    public DeleteExerciseCommand(Guid id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }
}