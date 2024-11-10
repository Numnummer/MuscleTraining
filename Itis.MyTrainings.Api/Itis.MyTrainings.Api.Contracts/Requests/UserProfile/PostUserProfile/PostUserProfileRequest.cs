using System.ComponentModel.DataAnnotations;
using Itis.MyTrainings.Api.Contracts.Enums;

namespace Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;

/// <summary>
/// Запрос на создания профиля пользователя
/// </summary>
public class PostUserProfileRequest
{
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Пол
    /// </summary>
    public Genders? Gender { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Рост
    /// </summary>
    public int? Height { get; set; }
    
    /// <summary>
    /// Вес
    /// </summary>
    public int? Weight { get; set; }
}