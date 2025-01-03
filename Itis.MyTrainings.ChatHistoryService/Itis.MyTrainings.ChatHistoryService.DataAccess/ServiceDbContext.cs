using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql;

public class ServiceDbContext : DbContext
{
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Files> MulticastFiles { get; set; }
    public DbSet<UnicastChatMessage> UnicastChatMessages { get; set; }
    public DbSet<Files> UnicastFiles { get; set; }

    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}