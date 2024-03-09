namespace Itis.MyTrainings.Api.Core.Exceptions;

/// <summary>
/// Исключение о незаполненном обязательном поле
/// </summary>
public class RequiredFieldIsEmpty: ApplicationExceptionBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="fieldName">Наименование поля</param>
    public RequiredFieldIsEmpty(string fieldName)
        : base($"Не заполнено обязательное поле '{fieldName}'")
    {
    }
}