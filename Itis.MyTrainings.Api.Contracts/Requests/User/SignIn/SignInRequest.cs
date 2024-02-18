using System.ComponentModel.DataAnnotations;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.SignIn;

/// <summary>
/// Запрос на вход
/// </summary>
public class SignInRequest
{
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