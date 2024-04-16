namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;

/// <summary>
/// Запрос на авторизацию пользователя через вконтакте
/// </summary>
public class RegisterUserWithVkRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="accessToken">Код авторизации</param>
    public RegisterUserWithVkRequest(string accessToken)
    {
        AccessToken = accessToken;
    }

    /// <summary>
    /// Код авторизации
    /// </summary>
    public string? AccessToken { get; set; }
}