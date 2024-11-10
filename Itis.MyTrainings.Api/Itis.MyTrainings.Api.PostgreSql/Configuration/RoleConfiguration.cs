using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="Role"/>
/// </summary>
internal class RoleConfiguration: IEntityTypeConfiguration<Role>
{
    private const string GuidCommand = "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)";
    
    /// <summary>
    /// Конфигурация сущности
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles", "public")
            .HasComment("Роли");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasDefaultValueSql(GuidCommand);

        builder.Property(p => p.Name)
            .HasComment("Наименование роли");

        builder.Property(p => p.NormalizedName)
            .HasComment("Нормализованное имя");
    }
}