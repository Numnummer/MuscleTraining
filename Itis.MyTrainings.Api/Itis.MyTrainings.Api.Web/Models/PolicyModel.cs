namespace Itis.MyTrainings.Api.Web.Models;

/// <summary>
/// Модель политики
/// </summary>
public class PolicyModel
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="policy">Политика</param>
    public PolicyModel(string policy)
    {
        Policy = policy;
        Roles = new List<string>();
    }

    /// <summary>
    /// Политика
    /// </summary>
    public string Policy { get; }

    /// <summary>
    /// Роли
    /// </summary>
    public List<string> Roles { get; }

    /// <summary>
    /// Добавить роли
    /// </summary>
    /// <param name="roles">Роли</param>
    public PolicyModel AddRoles(params string[] roles)
    {
        Roles.AddRange(roles);
        return this;
    }
}