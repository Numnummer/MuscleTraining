using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.SupportChat.LoadUnicastChatHistory;

public class LoadUnicastChatHistoryQuery : IRequest<LoadChatHistoryResponse[]>
{
    public LoadUnicastChatHistoryQuery(string email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public string Email { get; set; }
}