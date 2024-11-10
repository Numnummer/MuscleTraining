using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;


/// <summary>
/// Ответ на запрос <see cref="RegisterUserWithVkRequest"/>
/// </summary>
public class RegisterUserWithVkResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="result">Результат регистрации</param>
    public RegisterUserWithVkResponse(
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