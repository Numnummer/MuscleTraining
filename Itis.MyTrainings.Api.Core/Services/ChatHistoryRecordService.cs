using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using Microsoft.EntityFrameworkCore.Storage;

namespace Itis.MyTrainings.Api.Core.Services;

public class ChatHistoryRecordService : IChatHistoryRecordService
{
    private readonly IDbContext _dbContext;

    public ChatHistoryRecordService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RecordMessage(ChatMessage message)
    {
        await _dbContext.ChatMessages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RecordUnicastMessage(UnicastChatMessage message)
    {
        await _dbContext.UnicastChatMessages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }
}