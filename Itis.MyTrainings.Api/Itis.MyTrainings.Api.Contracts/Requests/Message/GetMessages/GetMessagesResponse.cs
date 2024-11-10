namespace Itis.MyTrainings.Api.Contracts.Requests.Message.GetMessages;

/// <summary>
/// Ответ на запрос на получение сообщений пользователя
/// </summary>
public class GetMessagesResponse
{
    /// <summary>
    /// конструктор
    /// </summary>
    /// <param name="items">Элементы ответа</param>
    public GetMessagesResponse(List<GetMessagesResponseItem> items)
    {
        Items = items;
    }
    
    /// <summary>
    /// Элементы ответа
    /// </summary>
    public List<GetMessagesResponseItem> Items { get; set; }
}