using System.Reflection;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Entities;

namespace Itis.MyTrainings.Api.Core.Managers;

/// <summary>
/// Менеджер ролей
/// </summary>
public static class RoleManager
{
    /// <summary>
    /// Метод для получения ролей из констант
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Role> GetRoles()
    {
        var roleConstants = typeof(Roles)
            .GetFields(BindingFlags.Public |
                       BindingFlags.Static |
                       BindingFlags.FlattenHierarchy)
            .Where(f => f is { IsLiteral: true, IsInitOnly: false })
            .ToArray();

        return roleConstants.Select(x => new Role
        {
            Id = Guid.NewGuid(),
            Name = x.GetValue(null)!.ToString(),
            NormalizedName = x.GetValue(null)!.ToString()!.ToUpper()
        });
    }
}