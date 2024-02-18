using System.ComponentModel.DataAnnotations;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;

/// <summary>
/// Запрос на регистрацию пользователя
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// Ник пользователя
    /// </summary>
    [Required]
    public string UserName { get; set; } = default!;
    
    /// <summary>
    /// Имя
    /// </summary>
    [Required]
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Фамилия
    /// </summary>
    [Required]
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Роль
    /// </summary>
    [Required]
    public string Role { get; set; } = default!;

    /// <summary>
    /// Почта
    /// </summary>
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}