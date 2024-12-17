using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql.Configurations;

public class UnicastChatMessageConfiguration : IEntityTypeConfiguration<UnicastChatMessage>
{
    public void Configure(EntityTypeBuilder<UnicastChatMessage> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(chatMessage => chatMessage.MessageText).HasMaxLength(200);
        builder.Property(chatMessage=>chatMessage.FromEmail).HasMaxLength(30);
        builder.Property(chatMessage=>chatMessage.ToEmail).HasMaxLength(30);
    }
}