using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;

/// <summary>
/// Ответ на запрос <see cref="RegisterUserRequest"/>
/// </summary>
public class RegisterUserResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="result">Результат регистрации</param>
    public RegisterUserResponse(IdentityResult result)
    {
        Result = result;
    }

    /// <summary>
    /// Результат регистрации
    /// </summary>
    public IdentityResult Result { get; }
}