using Itis.MyTrainings.Api.Core.Abstractions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.SignOut;

public class SignOutCommandHandler: IRequestHandler<SignOutCommand>
{
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    public SignOutCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _userService.SignOutAsync();
        return default;
    }
}