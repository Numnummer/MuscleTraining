using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;

namespace Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;

public interface IChatHistoryRecordService
{
    Task RecordMessage(ChatMessage message, string[] fileNames, byte[][] filesContent);
    Task RecordUnicastMessage(UnicastChatMessage message, string[] fileNames, byte[][] filesContent);
}