namespace Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;

public class LoadChatHistoryResponse
{
    public LoadChatHistoryResponse(string messageText, string senderEmail, DateTime sentDateTime)
    {
        MessageText = messageText ?? throw new ArgumentNullException(nameof(messageText));
        SenderEmail = senderEmail ?? throw new ArgumentNullException(nameof(senderEmail));
        SentDateTime = sentDateTime;
    }

    public string MessageText { get; set; }
    public string SenderEmail { get; set; }
    public DateTime SentDateTime { get; set; }
    
    public string[]? FileNames { get; set; }
    public byte[][]? FilesContent { get; set; }
}