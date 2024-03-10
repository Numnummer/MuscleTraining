using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="User"/>>
/// </summary>
internal class UserConfiguration: IEntityTypeConfiguration<User>
{
    private const string GuidCommand = "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)";

    /// <summary>
    /// Конфигурация сущности
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "public")
            .HasComment("Профили пользователей");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasDefaultValueSql(GuidCommand);

        builder.Property(p => p.UserName)
            .HasComment("Никнейм пользователя");
        
        builder.Property(p => p.FirstName)
            .HasComment("Имя");
        
        builder.Property(p => p.LastName)
            .HasComment("Фамилия");
        
        builder.Property(p => p.Email)
            .HasComment("Почта");

        builder.HasOne(x => x.UserProfile)
            .WithOne(x => x.User)
            .HasForeignKey<UserProfile>(x => x.UserId)
            .HasPrincipalKey<User>(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}