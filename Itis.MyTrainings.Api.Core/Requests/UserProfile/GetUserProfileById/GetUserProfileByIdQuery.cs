using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.UserProfile.GetUserProfileById;

/// <summary>
/// Запрос на получение профиля пользователя
/// </summary>
public class GetUserProfileByIdQuery : IRequest<GetUserProfileByIdResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="Id">Id сущности</param>
    public GetUserProfileByIdQuery(Guid Id)
        => this.Id = Id;

    /// <summary>
    /// ИД
    /// </summary>
    public Guid Id { get; set; }
}