using Itis.MyTrainings.Api.Core.Exceptions;

namespace Itis.MyTrainings.Api.Core.Entities;

/// <summary>
/// Сообщение
/// </summary>
public class Message : EntityBase
{
    private User _sender;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="sendDate">Дата отправки</param>
    /// <param name="messageText">Текст сообщения</param>
    /// <param name="sender">Отправитель</param>
    public Message(
        DateTime sendDate,
        string messageText,
        Guid sender)
    {
        SendDate = sendDate;
        SenderId = sender;
        MessageText = messageText;
    }

    private Message()
    {
    }
    
    /// <summary>
    /// Дата отправки
    /// </summary>
    public DateTime SendDate { get; set; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string MessageText { get; set; }

    /// <summary>
    /// Id отправителя
    /// </summary>
    public Guid SenderId { get; set; }
    
    /// <summary>
    /// Отправитель
    /// </summary>
    public User Sender
    {
        get => _sender;
        set
        {
            _sender = value 
                      ?? throw new RequiredFieldIsEmpty("Отправитель");
            SenderId = value.Id;
        }
    }
}