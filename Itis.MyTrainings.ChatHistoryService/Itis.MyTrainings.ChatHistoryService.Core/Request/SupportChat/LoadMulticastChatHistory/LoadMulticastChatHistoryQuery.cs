using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.LoadMulticastChatHistory;
using MediatR;

namespace Itis.MyTrainings.ChatHistoryService.Core.Request.SupportChat.LoadMulticastChatHistory;

public class LoadMulticastChatHistoryQuery : IRequest<LoadChatHistoryResponse[]>
{
    public LoadMulticastChatHistoryQuery(string group)
    {
        Group = group;
    }

    public string Group  { get; set; }
    
}