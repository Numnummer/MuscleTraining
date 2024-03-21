using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.SignIn;

/// <summary>
/// Ответ на запрос <see cref="SignInRequest"/>
/// </summary>
public class SignInResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="result">Результат входа</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="jwtToken">Jwt</param>
    public SignInResponse(
        SignInResult result, Guid userId, string jwtToken = default!)
    {
        JwtToken = jwtToken;
        Result = result;
        UserId = userId;
    }
    
    /// <summary>
    /// Jwt
    /// </summary>
    public string? JwtToken { get; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Результат входа
    /// </summary>
    public SignInResult Result { get; }
}