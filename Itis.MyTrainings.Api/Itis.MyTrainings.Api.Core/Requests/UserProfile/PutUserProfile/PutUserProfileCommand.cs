using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PutUserProfile;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.UserProfile.PutUserProfile;

/// <summary>
/// Запрос на изменение профиля пользователя
/// </summary>
public class PutUserProfileCommand: PutUserProfileRequest, IRequest<PutUserProfileResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    public PutUserProfileCommand(Guid userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}