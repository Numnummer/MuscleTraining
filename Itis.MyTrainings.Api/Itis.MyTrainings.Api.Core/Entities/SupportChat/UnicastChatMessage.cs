namespace Itis.MyTrainings.Api.Core.Entities.SupportChat;

public class UnicastChatMessage : EntityBase
{
    public string FromEmail { get; set; }
    public string ToEmail { get; set; }
    public string MessageText { get; set; }
    public DateTime SendDate { get; set; }
}