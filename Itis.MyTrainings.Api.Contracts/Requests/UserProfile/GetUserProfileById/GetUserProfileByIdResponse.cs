namespace Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;

/// <summary>
/// Ответ на зпрос на получение профиля пользователя
/// </summary>
public class GetUserProfileByIdResponse
{
    /// <summary>
    /// Пол
    /// </summary>
    public string? Gender { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Почта
    /// </summary>
    public string? Email { get; set; }
    
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