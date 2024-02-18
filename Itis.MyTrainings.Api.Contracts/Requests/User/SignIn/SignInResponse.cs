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
    /// <param name="jwtToken">Jwt</param>
    public SignInResponse(
        SignInResult result,
        string jwtToken = default!)
    {
        JwtToken = jwtToken;
        Result = result;
    }
    
    /// <summary>
    /// Jwt
    /// </summary>
    public string? JwtToken { get; }
    
    /// <summary>
    /// Результат входа
    /// </summary>
    public SignInResult Result { get; }
}