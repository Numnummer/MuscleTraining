namespace Itis.MyTrainings.Api.Core.Abstractions;

/// <summary>
/// Сервис для работы с Email
/// </summary>
public interface IEmailSenderService
{
    /// <summary>
    /// Отправить сообщение на почту
    /// </summary>
    /// <param name="subject">Заголовок сообщения</param>
    /// <param name="body">Тело сообщения</param>
    /// <param name="sendTo">Получатель</param>
    /// <returns>-</returns>
    public Task SendMessageAsync(string? subject, string body, string sendTo, Dictionary<string, string>? placeholders, CancellationToken cancellationToken);
}