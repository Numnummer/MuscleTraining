using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.SupportChat.LoadUnicastChatHistory;

public class LoadUnicastChatHistoryQuery : IRequest<LoadChatHistoryResponse[]>
{
    public LoadUnicastChatHistoryQuery(string firstEmail, string secondEmail)
    {
        FirstEmail = firstEmail ?? throw new ArgumentNullException(nameof(firstEmail));
        SecondEmail = secondEmail ?? throw new ArgumentNullException(nameof(secondEmail));
    }

    public string FirstEmail { get; set; }
    public string SecondEmail { get; set; }
}