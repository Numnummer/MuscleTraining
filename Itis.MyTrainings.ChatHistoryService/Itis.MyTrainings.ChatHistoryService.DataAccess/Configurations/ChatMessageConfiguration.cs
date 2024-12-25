using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql.Configurations;

internal class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(chatMessage => chatMessage.MessageText).HasMaxLength(200);
        builder.Property(chatMessage=>chatMessage.SenderEmail).HasMaxLength(30);
    }
}