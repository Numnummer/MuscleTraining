namespace Itis.MyTrainings.Api.Contracts.Requests.Message.GetMessages;

/// <summary>
/// Элемент ответа для <see cref="GetMessagesResponse"/>
/// </summary>
public class GetMessagesResponseItem
{
    /// <summary>
    /// Имя отправителя
    /// </summary>
    public string SenderName { get; set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string MesssageText { get; set; }

    /// <summary>
    /// Дата отправки
    /// </summary>
    public DateTime SendDate { get; set; }
}