namespace Itis.MyTrainings.Api.Core.Constants;

/// <summary>
/// Роли (При добавлении новых ролей создавать миграцию)
/// </summary>
public static class Roles
{
    /// <summary>
    /// Администратор
    /// </summary>
    public const string Administrator = "Administrator";

    /// <summary>
    /// Тренер
    /// </summary>
    public const string Coach = "Coach";

    /// <summary>
    /// Обычный пользователь
    /// </summary>
    public const string User = "User";
}