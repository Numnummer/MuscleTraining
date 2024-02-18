using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Services;

/// <inheritdoc />
public class RoleService: IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="roleManager">Менеджер ролей</param>
    public RoleService(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    /// <inheritdoc />
    public async Task<bool> IsRoleExistAsync(string roleName)
        => await _roleManager.RoleExistsAsync(roleName);
}