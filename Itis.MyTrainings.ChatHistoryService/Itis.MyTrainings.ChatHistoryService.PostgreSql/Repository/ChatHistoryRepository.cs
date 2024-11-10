using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Enums;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.LoadMulticastChatHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql.Repository;

public class ChatHistoryRepository : IChatHistoryRepository
{
    private readonly ServiceDbContext _dbContext;
    private readonly ILogger<ChatHistoryRepository> _logger;

    public ChatHistoryRepository(ServiceDbContext dbContext, ILogger<ChatHistoryRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<LoadChatHistoryResponse[]> LoadUnicastChatHistoryAsync(string firstEmail, string secondEmail)
    {
        return await _dbContext.UnicastChatMessages
            .Where(msg =>
                (msg.FromEmail == firstEmail
                 || msg.FromEmail == secondEmail)
                &&(msg.ToEmail == firstEmail
                   || msg.ToEmail == secondEmail))
            .Select(msg => new LoadChatHistoryResponse(msg.MessageText, msg.FromEmail, msg.SendDate))
            .ToArrayAsync();
    }

    public async Task<LoadChatHistoryResponse[]> LoadMulticastChatHistoryAsync(string groupString)
    {
        if (!Enum.TryParse(typeof(Group), groupString, true, out var group))
            throw new ArgumentException($"Не найдено сообщение с группой {groupString}");
        
        return await _dbContext.ChatMessages
            .Where(msg => msg.GroupName == (Group)group)
            .Select(msg=>
                new LoadChatHistoryResponse(msg.MessageText,msg.SenderEmail, msg.SendDate))
            .ToArrayAsync();
    }

    public async Task RecordMessageAsync(ChatMessage message)
    {
        await _dbContext.ChatMessages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RecordUnicastMessageAsync(UnicastChatMessage message)
    {
        await _dbContext.UnicastChatMessages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Unicast record");
    }
}