using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация сущности <see cref="Message"/>
/// </summary>
internal class MessageConfiguration : EntityBaseConfiguration<Message>
{
    /// <inheritdoc/>
    public override void ConfigureChild(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages", "public")
            .HasComment("Сообщения");
        
        builder.Property(p => p.MessageText)
            .HasComment("Текст сообщения");
        
        builder.Property(p => p.RecieverId)
            .HasComment("Id поулчателя");
        
        builder.Property(p => p.SenderId)
            .HasComment("Id отправителя");
        
        builder.Property(p => p.SendDate)
            .HasComment("Дата отправки");

        builder.HasOne(x => x.Sender)
            .WithMany(x => x.SendedMessages)
            .HasForeignKey(x => x.SenderId)
            .HasPrincipalKey(x => x.Id);
        
        builder.HasOne(x => x.Reciever)
            .WithMany(x => x.RecievedMessages)
            .HasForeignKey(x => x.RecieverId)
            .HasPrincipalKey(x => x.Id);
    }
}