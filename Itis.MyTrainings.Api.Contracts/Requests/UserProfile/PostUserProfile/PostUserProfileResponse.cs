namespace Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;

/// <summary>
/// Ответ на запрос <see cref="PostUserProfileRequest"/>
/// </summary>
public class PostUserProfileResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userProfileId">Идентификатор профиля пользователя</param>
    public PostUserProfileResponse(Guid userProfileId)
    {
        UserProfileId = userProfileId;
    }
    
    /// <summary>
    /// Идентификатор профиля пользователя
    /// </summary>
    public Guid UserProfileId { get; set; }
}