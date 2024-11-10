using Itis.MyTrainings.Api.Contracts.Requests.User.GetCurrentUserInfo;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.GetCurrentUserInfo;

/// <summary>
/// Команда запроса
/// </summary>
public class GetCurrentUserInfoQuery: IRequest<GetCurrentUserInfoResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    public GetCurrentUserInfoQuery(Guid? userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid? UserId { get; set; }
}