using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.PostgreSql.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Конфигурация моделей при запуске
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            RoleManager.GetRoles()
        );

        modelBuilder.Entity<IdentityUserRole<Guid>>(userRole =>
        {
            userRole.HasKey(pr => new
            {
                pr.UserId,
                pr.RoleId,
            });
        });
        modelBuilder.Entity<IdentityUserLogin<Guid>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<Guid>>().HasNoKey();
    }
}