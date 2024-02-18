namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;

/// <summary>
/// Запрос на авторизацию пользователя через вконтакте
/// </summary>
public class RegisterUserWithVkRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="code">Код авторизации</param>
    public RegisterUserWithVkRequest(string code)
    {
        Code = code;
    }

    /// <summary>
    /// Код авторизации
    /// </summary>
    public string? Code { get; set; }
}