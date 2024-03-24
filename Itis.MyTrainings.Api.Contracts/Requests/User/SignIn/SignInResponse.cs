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
    /// <param name="refreshToken">Refresh token</param>
    public SignInResponse(
        SignInResult result, Guid userId, string jwtToken = default!, string refreshToken = default!)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
        Result = result;
        UserId = userId;
    }
    
    /// <summary>
    /// Jwt
    /// </summary>
    public string JwtToken { get; }

    /// <summary>
    /// Refresh token
    /// </summary>
    public string RefreshToken { get; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; }
    
    /// <summary>
    /// Результат входа
    /// </summary>
    public SignInResult Result { get; }
}