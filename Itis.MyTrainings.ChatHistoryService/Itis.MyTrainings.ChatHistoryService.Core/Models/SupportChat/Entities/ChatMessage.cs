using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Enums;

namespace Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;

public class ChatMessage
{
    public Guid Id { get; set; }
    public string MessageText { get; set; }
    public string SenderEmail { get; set; }
    public DateTime SendDate { get; set; }
    public Group? GroupName { get; set; }
}