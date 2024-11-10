using Itis.MyTrainings.Api.Contracts.Requests.Training.PostTraining;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Training.PostTraining;

/// <summary>
/// Запрос на создание Тренировки
/// </summary>
public class PostTrainingCommand: PostTrainingRequest, IRequest<PostTrainingResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    public PostTrainingCommand(Guid userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}