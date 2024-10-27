namespace Itis.MyTrainings.Api.Core.Entities.SupportChat;

public class ChatMessage : EntityBase
{
    public string MessageText { get; set; }
    public string SenderEmail { get; set; }
    public DateTime SendDate { get; set; }
    public Group? GroupName { get; set; }
}