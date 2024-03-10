using Itis.MyTrainings.Api.Contracts.Enums;

namespace Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;

/// <summary>
/// Ответ на зпрос на получение профиля пользователя
/// </summary>
public class GetUserProfileByIdResponse
{
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
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Рос
    /// </summary>
    public int? Height { get; set; }
    
    /// <summary>
    /// Вес
    /// </summary>
    public int? Weight { get; set; }
    
    /// <summary>
    /// Дата создания профиля
    /// </summary>
    public DateTime CreateDate { get; set; }
}