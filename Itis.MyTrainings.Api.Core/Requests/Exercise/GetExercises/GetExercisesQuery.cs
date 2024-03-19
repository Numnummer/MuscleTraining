using Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExercises;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Exercise.GetExercises;

/// <summary>
/// Запрос получения упражнений
/// </summary>
public class GetExercisesQuery: GetExercisesRequest, IRequest<GetExercisesResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    public GetExercisesQuery(Guid userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}