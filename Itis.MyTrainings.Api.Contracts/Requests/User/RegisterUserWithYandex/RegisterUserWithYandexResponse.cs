using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithYandex;

/// <summary>
/// Ответ на запрос <see cref="RegisterUserWithYandexRequest"/>
/// </summary>

public class RegisterUserWithYandexResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="result">Результат регистрации</param>
    public RegisterUserWithYandexResponse(
        IdentityResult result,
        string? jwt)
    {
        Result = result;
        Jwt = jwt;
    }

    /// <summary>
    /// Результат регистрации
    /// </summary>
    public IdentityResult Result { get; }
    
    /// <summary>
    /// Jwt
    /// </summary>
    public string? Jwt { get; set; }
}