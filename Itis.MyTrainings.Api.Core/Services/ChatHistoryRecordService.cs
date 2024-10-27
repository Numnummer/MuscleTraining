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
    }
}