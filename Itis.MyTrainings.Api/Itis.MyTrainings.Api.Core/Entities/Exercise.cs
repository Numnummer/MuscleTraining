using Itis.MyTrainings.Api.Core.Exceptions;

namespace Itis.MyTrainings.Api.Core.Entities;

/// <summary>
/// Упражнение
/// </summary>
public class Exercise : EntityBase
{
    private User _user;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Пользователь</param>
    /// <param name="name">Название</param>
    public Exercise(
        Guid userId,
        string name)
    {
        UserId = userId;
        Name = name;
        Trainings = new List<Training>();
    }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Кол-во подходов
    /// </summary>
    public int? Approaches { get; set; }

    /// <summary>
    /// Кол-во повторений в подходе
    /// </summary>
    public int? Repetitions { get; set; }

    /// <summary>
    /// Ход выполнения
    /// </summary>
    public string? ImplementationProgress { get; set; }

    /// <summary>
    /// Ссылка на видео с объяснением
    /// </summary>
    public string? ExplanationVideo { get; set; }

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
    /// Тренировки
    /// </summary>
    public List<Training> Trainings { get; set; }
}