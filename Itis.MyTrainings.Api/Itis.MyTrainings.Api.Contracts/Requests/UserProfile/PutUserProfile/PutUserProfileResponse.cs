namespace Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PutUserProfile;

/// <summary>
/// Ответ на запрос <see cref="PutUserProfileRequest"/>
/// </summary>
public class PutUserProfileResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userProfileId">Идентификатор профиля пользователя</param>
    public PutUserProfileResponse(Guid userProfileId)
    {
        UserProfileId = userProfileId;
    }
    
    /// <summary>
    /// Идентификатор профиля пользователя
    /// </summary>
    public Guid UserProfileId { get; set; }
}