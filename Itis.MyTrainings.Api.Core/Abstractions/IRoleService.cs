using Itis.MyTrainings.Api.Core.Entities;

namespace Itis.MyTrainings.Api.Core.Abstractions;

/// <summary>
/// Сервис для работы с ролями
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Существует ли роль
    /// </summary>
    /// <param name="roleName">Наименование роли</param>
    /// <returns></returns>
    public Task<bool> IsRoleExistAsync(string roleName);
}