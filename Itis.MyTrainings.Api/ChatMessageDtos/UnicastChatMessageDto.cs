namespace ChatMessageDtos 
{
    public class UnicastChatMessageDto 
    {
        public Guid Id { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; }
        public string[] FileNames { get; set; }
        public byte[][] FilesContent { get; set; }
    }
}