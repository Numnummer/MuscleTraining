namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterWithGoogle;

public class RegisterWithGoogleResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="jwtToken">Jwt</param>
    public RegisterWithGoogleResponse(string jwtToken)
    {
        JwtToken = jwtToken;
    }
    
    /// <summary>
    /// Jwt
    /// </summary>
    public string JwtToken { get; }
}