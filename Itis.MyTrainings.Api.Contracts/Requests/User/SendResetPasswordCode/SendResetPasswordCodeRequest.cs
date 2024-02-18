using System.ComponentModel.DataAnnotations;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;
#nullable disable

/// <summary>
/// Запрос на отправку кода для сброса пароля
/// </summary>
public class SendResetPasswordCodeRequest
{
    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [DataType(DataType.EmailAddress)] 
    public string Email { get; set; }
}