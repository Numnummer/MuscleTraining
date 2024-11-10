using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;
using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;

namespace Itis.MyTrainings.ChatHistoryService.Core.Services;

public class ChatHistoryRecordService : IChatHistoryRecordService
{
    private readonly IChatHistoryRepository _chatHistoryRepository;

    public ChatHistoryRecordService(IChatHistoryRepository chatHistoryRepository)
    {
        _chatHistoryRepository = chatHistoryRepository;
    }

    public async Task RecordMessage(ChatMessage message)
    {
        await _chatHistoryRepository.RecordMessageAsync(message);
    }

    public async Task RecordUnicastMessage(UnicastChatMessage message)
    {
        await _chatHistoryRepository.RecordUnicastMessageAsync(message);
    }
}