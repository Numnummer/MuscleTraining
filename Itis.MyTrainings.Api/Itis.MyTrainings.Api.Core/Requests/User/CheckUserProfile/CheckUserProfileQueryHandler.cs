using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.CheckUserProfile;

/// <summary>
/// Обработчик запроса <see cref="CheckUserProfileQuery"/>
/// </summary>
public class CheckUserProfileQueryHandler: IRequestHandler<CheckUserProfileQuery, bool>
{
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    public CheckUserProfileQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    /// <inheritdoc />
    public async Task<bool> Handle(CheckUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindUserByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        return user.UserProfileId.HasValue;
    }
}