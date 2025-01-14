namespace ChatMessageDtos;

public class MulticastChatMessageDto
{
    public Guid Id { get; set; }
    public string MessageText { get; set; }
    public string SenderEmail { get; set; }
    public DateTime SendDate { get; set; }
    public Group? GroupName { get; set; }
    public string[] FileNames { get; set; }
    public byte[][] FilesContent { get; set; }
    public string[] FilesMetadata { get; set; }
}