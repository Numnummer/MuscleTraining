using Itis.MyTrainings.Api.Core.Entities.SupportChat;

namespace Itis.MyTrainings.Api.Core.Abstractions;

public interface IChatHistoryRecordService
{
    Task RecordMessage(ChatMessage message);
    Task RecordUnicastMessage(UnicastChatMessage message);
}