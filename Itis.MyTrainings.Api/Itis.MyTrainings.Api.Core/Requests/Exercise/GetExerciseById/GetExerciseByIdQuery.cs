using Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExerciseById;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Exercise.GetExerciseById;

/// <summary>
/// Запрос на получение упражнения по Id
/// </summary>
public class GetExerciseByIdQuery : IRequest<GetExerciseByIdResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="exerciseId">Идентификатор упражнения</param>
    public GetExerciseByIdQuery(Guid userId, Guid exerciseId)
    {
        UserId = userId;
        ExerciseId = exerciseId;
    }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Идентификатор упражнения
    /// </summary>
    public Guid ExerciseId { get; set; }
}