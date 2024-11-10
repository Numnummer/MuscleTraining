using Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainings;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Training.GetTrainings;

/// <summary>
/// Запрос получения списка тренировок
/// </summary>
public class GetTrainingsQuery: GetTrainingsRequest, IRequest<GetTrainingsResponse>
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public GetTrainingsQuery(
        Guid userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public Guid UserId { get; set; }
}