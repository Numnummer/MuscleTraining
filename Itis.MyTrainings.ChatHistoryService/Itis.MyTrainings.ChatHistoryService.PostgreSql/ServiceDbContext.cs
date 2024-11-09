using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql;

public class ServiceDbContext : DbContext
{
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<UnicastChatMessage> UnicastChatMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}