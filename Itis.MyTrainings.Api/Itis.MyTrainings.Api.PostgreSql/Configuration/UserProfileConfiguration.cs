using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="UserProfile"/>>
/// </summary>
internal class UserProfileConfiguration: EntityBaseConfiguration<UserProfile>
{
    /// <inheritdoc />
    public override void ConfigureChild(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles", "public")
            .HasComment("Профили пользователей");
        
        builder.Property(p => p.Gender)
            .HasComment("Гендер");
        
        builder.Property(p => p.PhoneNumber)
            .HasComment("Номер телефона");
        
        builder.Property(p => p.DateOfBirth)
            .HasComment("Дата рождения");
        
        builder.Property(p => p.Weight)
            .HasComment("Вес");
        
        builder.Property(p => p.Height)
            .HasComment("Рост");

        builder.HasOne(x => x.User)
            .WithOne(x => x.UserProfile)
            .HasForeignKey<UserProfile>(x => x.UserId)
            .HasPrincipalKey<User>(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}