using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithYandex;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithYandex;

/// <summary>
/// Команда запроса <see cref="RegisterUserWithYandexRequest"/>
/// </summary>
public class RegisterUserWithYandexCommand: RegisterUserWithYandexRequest, IRequest<RegisterUserWithYandexResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="code">Код авторизации</param>
    public RegisterUserWithYandexCommand(string code)
        : base(code){}
}