using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.PostgreSql.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.PostgreSql;

/// <summary>
/// Контекст EF Core для приложения
/// </summary>
public class EfContext: IdentityDbContext<User, Role, Guid>, IDbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options">Параметры подключения к БД</param>
    public EfContext(DbContextOptions<EfContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Добавление моделей при запуске
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
    
    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await SaveChangesAsync(true, cancellationToken);

    /// <inheritdoc />
    public DbSet<UserProfile> UserProfiles { get; set; }
}