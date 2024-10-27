using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.Api.PostgreSql.Configuration;

internal class ChatMessageConfiguration : EntityBaseConfiguration<ChatMessage>
{
    public override void ConfigureChild(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.Property(chatMessage => chatMessage.MessageText).HasMaxLength(200);
        builder.Property(chatMessage=>chatMessage.SenderEmail).HasMaxLength(30);
    }
}