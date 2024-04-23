using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Training.DeleteTraining;

/// <summary>
/// Запрос на удаление упражнения
/// </summary>
public class DeleteTrainingCommand: IRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    public DeleteTrainingCommand(Guid id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }
}