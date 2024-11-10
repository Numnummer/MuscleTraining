using Itis.MyTrainings.Api.Core.Exceptions;

namespace Itis.MyTrainings.Api.Core.Entities;

/// <summary>
/// Тренировка
/// </summary>
public class Training: EntityBase
{
    private User _user;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="trainingDate">Дата тренировки</param>
    public Training(
        Guid userId,
        DateTime trainingDate)
    {
        UserId = userId;
        TrainingDate = trainingDate;
        Exercises = new List<Exercise>();
    }
    
    /// <summary>
    /// Наименование тренировки
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Дата тренировки
    /// </summary>
    public DateTime TrainingDate { get; set; }
    
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

    /// <summary>
    /// Упражнения
    /// </summary>
    public List<Exercise> Exercises { get; set; }
}