using System.ComponentModel;

namespace Itis.MyTrainings.Api.Core.Extensions;

/// <summary>
/// Расширения для <see cref="Enum"/>
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Получить описание из <see cref="DescriptionAttribute"/>
    /// </summary>
    /// <param name="source">Enum</param>
    /// <returns>Описание</returns>
    public static string? GetDescription(this Enum source)
    {
        var member = source.GetType().GetMember(source.ToString()).FirstOrDefault();
        var attribute = member?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

        return (attribute as DescriptionAttribute)?.Description;
    }
}