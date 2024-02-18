using Itis.MyTrainings.Api.Core.Entities;
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
    /// Сохранить изменения в БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество обновленных записей</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}