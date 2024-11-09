using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.Api.Core.Requests.SupportChat.LoadUnicastChatHistory;
using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;
using MediatR;

namespace Itis.MyTrainings.ChatHistoryService.Core.Request.SupportChat.LoadUnicastChatHistory;

public class LoadUnicastChatHistoryHandler : IRequestHandler<LoadUnicastChatHistoryQuery, LoadChatHistoryResponse[]>
{
    private readonly IChatHistoryRepository _chatHistoryRepository;

    public LoadUnicastChatHistoryHandler(IChatHistoryRepository chatHistoryRepository)
    {
        _chatHistoryRepository = chatHistoryRepository;
    }


    public async Task<LoadChatHistoryResponse[]> Handle(LoadUnicastChatHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _chatHistoryRepository.LoadUnicastChatHistoryAsync(request.FirstEmail,request.SecondEmail);
    }
}