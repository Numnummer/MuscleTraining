using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.CheckUserProfile;

/// <summary>
/// Запрос на проверку существования профиля пользователя
/// </summary>
public class CheckUserProfileQuery: IRequest<bool>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    public CheckUserProfileQuery(Guid userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}