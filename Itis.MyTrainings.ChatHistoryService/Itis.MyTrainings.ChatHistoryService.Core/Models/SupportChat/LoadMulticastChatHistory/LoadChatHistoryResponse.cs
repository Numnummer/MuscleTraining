using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using StorageS3Shared;

namespace Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.LoadMulticastChatHistory;

public class LoadChatHistoryResponse
{
    public LoadChatHistoryResponse(string messageText, string senderEmail, DateTime sentDateTime, Files[] file)
    {
        MessageText = messageText ?? throw new ArgumentNullException(nameof(messageText));
        SenderEmail = senderEmail ?? throw new ArgumentNullException(nameof(senderEmail));
        SentDateTime = sentDateTime;
        Files = file;
    }

    public string MessageText { get; set; }
    public string SenderEmail { get; set; }
    public DateTime SentDateTime { get; set; }
    public Files[] Files { get; set; }
}