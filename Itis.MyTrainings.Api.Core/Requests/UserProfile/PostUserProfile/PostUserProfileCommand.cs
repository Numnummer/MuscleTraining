using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.UserProfile.PostUserProfile;

/// <summary>
/// Команда запроса для <see cref="PostUserProfileRequest"/>
/// </summary>
public class PostUserProfileCommand: PostUserProfileRequest, IRequest<PostUserProfileResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    public PostUserProfileCommand(Guid userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}