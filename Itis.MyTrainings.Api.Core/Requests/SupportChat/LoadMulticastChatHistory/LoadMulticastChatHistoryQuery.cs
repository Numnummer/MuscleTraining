using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.SupportChat;

public class LoadMulticastChatHistoryQuery : IRequest<LoadChatHistoryResponse[]>
{
    public LoadMulticastChatHistoryQuery(string group)
    {
        Group = group;
    }

    public string Group  { get; set; }
    
}