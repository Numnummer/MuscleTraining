using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;

namespace Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;

public interface IChatHistoryRepository
{
    Task<LoadChatHistoryResponse[]> LoadUnicastChatHistoryAsync(string firstEmail, string secondEmail);
    Task<LoadChatHistoryResponse[]> LoadMulticastChatHistoryAsync(string group);
    Task RecordMessageAsync(ChatMessage message);
    Task RecordUnicastMessageAsync(UnicastChatMessage message);
}