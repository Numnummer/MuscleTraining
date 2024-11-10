namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithYandex;

/// <summary>
/// Запрос на авторизацию через Яндекс.
/// </summary>
public class RegisterUserWithYandexRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="code">Код авторизации</param>
    public RegisterUserWithYandexRequest(string code)
    {
        Code = code;
    }

    /// <summary>
    /// Код авторизации
    /// </summary>
    public string? Code { get; set; }
}