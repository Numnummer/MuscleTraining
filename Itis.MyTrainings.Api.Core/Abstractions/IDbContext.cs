using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Abstractions;

/// <summary>
/// Контекст EF Core для приложения
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Профиль пользователя
    /// </summary>
    DbSet<UserProfile> UserProfiles { get; set; }

    /// <summary>
    /// Упражнения
    /// </summary>
    DbSet<Exercise> Exercises { get; set; }

    /// <summary>
    /// Тренировки
    /// </summary>
    DbSet<Training> Trainings { get; set; }
    
    /// <summary>
    /// Сообщения
    /// </summary>
    DbSet<Message> Messages { get; set; }
    
    DbSet<ChatMessage> ChatMessages { get; set; }
    
    DbSet<UnicastChatMessage> UnicastChatMessages { get; set; }
    
    /// <summary>
    /// Сохранить изменения в БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество обновленных записей</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}