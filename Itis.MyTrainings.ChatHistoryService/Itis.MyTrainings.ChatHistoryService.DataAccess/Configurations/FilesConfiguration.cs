using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql.Configurations;

public class FilesConfiguration : IEntityTypeConfiguration<Files>
{
    public void Configure(EntityTypeBuilder<Files> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.FileName).HasMaxLength(100).IsRequired();
    }
}