using System.ComponentModel.DataAnnotations;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.ResetPassword;

/// <summary>
/// Запрос на сброс пароля
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// Email
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
    
    /// <summary>
    /// Код подтверждения
    /// </summary>
    [Required]
    public string Code { get; set; } = default!;
}