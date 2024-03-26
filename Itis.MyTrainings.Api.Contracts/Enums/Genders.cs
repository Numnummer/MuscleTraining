using System.ComponentModel;

namespace Itis.MyTrainings.Api.Contracts.Enums;

/// <summary>
/// Гендеры
/// </summary>
public enum Genders
{
    /// <summary>
    /// Мужчина
    /// </summary>
    [Description("Мужчина")]
    Man = 0,
     
    /// <summary>
    /// Женщина
    /// </summary>
    [Description("Женщина")]
    Woman = 1,
}