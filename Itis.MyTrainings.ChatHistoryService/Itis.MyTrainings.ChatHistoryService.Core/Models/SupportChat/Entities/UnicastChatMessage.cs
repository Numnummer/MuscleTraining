namespace Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;

public class UnicastChatMessage
{
    public Guid Id { get; set; }
    public string FromEmail { get; set; }
    public string ToEmail { get; set; }
    public string MessageText { get; set; }
    public DateTime SendDate { get; set; }
}