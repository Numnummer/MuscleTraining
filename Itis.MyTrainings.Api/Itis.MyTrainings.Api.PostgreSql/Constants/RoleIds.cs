namespace Itis.MyTrainings.Api.PostgreSql.Constants;

/// <summary>
/// Идентификаторы ролей
/// </summary>
public static class RoleIds
{
    /// <summary>
    /// Администратор
    /// </summary>
    public static Guid Administrator = new ("3a96e520-caf4-464d-85e8-304863711e7b");
    
    /// <summary>
    /// Тренер
    /// </summary>
    public static Guid Coach = new ("d64e70fa-8ace-4eba-8753-2c2f383c61b3");
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public static Guid User = new ("eedd2ec5-1b1d-4ba7-9001-16db15898319");
}