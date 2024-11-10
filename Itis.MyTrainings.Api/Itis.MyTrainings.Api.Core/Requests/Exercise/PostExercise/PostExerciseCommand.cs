using Itis.MyTrainings.Api.Contracts.Requests.Exercise.PostExercise;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Exercise.PostExercise;

/// <summary>
/// Команда запроса для <see cref="PostExerciseRequest"/>
/// </summary>
public class PostExerciseCommand: PostExerciseRequest, IRequest<PostExerciseResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Пользователь</param>
    public PostExerciseCommand(Guid userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public Guid UserId { get; set; }
}