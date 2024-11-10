using System.ComponentModel;
using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Enums;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.LoadMulticastChatHistory;
using MediatR;

namespace Itis.MyTrainings.ChatHistoryService.Core.Request.SupportChat.LoadMulticastChatHistory;

public class LoadMulticastChatHistoryHandler : 
    IRequestHandler<LoadMulticastChatHistoryQuery,LoadChatHistoryResponse[]>
{
    private readonly IChatHistoryRepository _chatHistoryRepository;

    public LoadMulticastChatHistoryHandler(IChatHistoryRepository chatHistoryRepository)
    {
        _chatHistoryRepository = chatHistoryRepository;
    }


    public async Task<LoadChatHistoryResponse[]> Handle(LoadMulticastChatHistoryQuery request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(typeof(Group), request.Group, true, out var group))
            throw new InvalidEnumArgumentException($"Не найдено сообщение с группой {request.Group}");
        
        return await _chatHistoryRepository.LoadMulticastChatHistoryAsync(request.Group);
    }
}