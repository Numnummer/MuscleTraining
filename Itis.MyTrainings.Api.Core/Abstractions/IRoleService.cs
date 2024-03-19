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

    /// <summary>
    /// Получить поль по id
    /// </summary>
    /// <param name="roleId">Идентификатор роли</param>
    /// <returns>Роль</returns>
    public Task<Role?> GetRoleById(Guid roleId);
}