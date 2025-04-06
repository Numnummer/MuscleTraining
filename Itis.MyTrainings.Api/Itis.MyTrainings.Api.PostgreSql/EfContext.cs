using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
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
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    /// <summary>
    /// Добавление моделей при запуске
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfContext).Assembly);
    }

    // <inheritdoc />
    public DbSet<UnicastChatMessage> UnicastChatMessages { get; set; }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await SaveChangesAsync(true, cancellationToken);

    /// <inheritdoc />
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    /// <inheritdoc />
    public DbSet<Exercise> Exercises { get; set; }

    /// <inheritdoc />
    public DbSet<Training> Trainings { get; set; }
    
    /// <inheritdoc />
    public DbSet<Message> Messages { get; set; }
    
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Product> Products { get; set; }
}