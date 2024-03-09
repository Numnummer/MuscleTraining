using Itis.MyTrainings.Api.Core.Exceptions;

namespace Itis.MyTrainings.Api.Core.Entities;

/// <summary>
/// Профиль пользователя
/// </summary>
public class UserProfile : EntityBase
{
    private User _user;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public UserProfile()
    {
    }
    
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
    /// Рост
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

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public User User 
    { 
        get => _user;
        set
        {
            _user = value 
                ?? throw new RequiredFieldIsEmpty("Пользователь");
            UserId = value.Id;
        }
    }
}