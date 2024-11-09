using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;

namespace Itis.MyTrainings.Api.Core.Abstractions;

public interface IChatHistoryRecordService
{
    Task RecordMessage(ChatMessage message);
    Task RecordUnicastMessage(UnicastChatMessage message);
}