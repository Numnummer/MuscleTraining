using System.Reflection;
using HtmlAgilityPack;

namespace Itis.MyTrainings.Api.Core.Managers;

/// <summary>
/// Менеджер для работы с Html файлами
/// </summary>
public static class HtmlFileManager
{
    /// <summary>
    /// Получить тело из html файла
    /// </summary>
    /// <param name="htmlFilePath">Путь до html файла</param>
    /// <param name="fileName">Имя файла</param>
    /// <returns></returns>
    public static string GetHtmlFileBody(string htmlFilePath, string fileName)
    {
        if (string.IsNullOrEmpty(htmlFilePath))
            throw new ArgumentNullException(htmlFilePath);
        
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentNullException(fileName);
            
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{htmlFilePath}.{fileName}")
            ?? throw new FileNotFoundException($"Файл с названием {fileName} не найден");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}