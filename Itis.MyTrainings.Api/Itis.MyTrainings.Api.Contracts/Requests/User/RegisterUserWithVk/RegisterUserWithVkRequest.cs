using System.Security.AccessControl;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;

/// <summary>
/// Запрос на авторизацию пользователя через вконтакте
/// </summary>
public class RegisterUserWithVkRequest
{
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; }
}