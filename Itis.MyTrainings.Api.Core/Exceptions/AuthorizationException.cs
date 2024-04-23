namespace Itis.MyTrainings.Api.Core.Exceptions;

/// <summary>
/// Ошибка авторизации
/// </summary>
public class AuthorizationException: ApplicationExceptionBase
{
    /// <summary>
    /// Исключение при некорректной авторизации
    /// </summary>
    public AuthorizationException()
        : base("Не удалось войти в аккаунт. Проверьте корректность почты и пароля.")
    {
    }
}