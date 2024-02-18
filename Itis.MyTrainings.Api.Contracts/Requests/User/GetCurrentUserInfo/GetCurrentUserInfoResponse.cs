using Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.GetCurrentUserInfo;

/// <summary>
/// Ответ на запрос <see cref="SendResetPasswordCodeRequest"/>
/// </summary>
public class GetCurrentUserInfoResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор профиля пользователя
    /// </summary>
    public Guid? UserProfileId { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = default!;
        
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Пол
    /// </summary>
    public string? Gender { get; set; }
    
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
    /// Дата начала тренировок
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Количество тренировок в неделю
    /// </summary>
    public int? WeeklyTrainingFrequency { get; set; }
    
    /// <summary>
    /// Заболевания
    /// </summary>
    public string? MedicalSickness { get; set; }
    
    /// <summary>
    /// Предпочтения по питанию
    /// </summary>
    public string? DietaryPreferences { get; set; }
}