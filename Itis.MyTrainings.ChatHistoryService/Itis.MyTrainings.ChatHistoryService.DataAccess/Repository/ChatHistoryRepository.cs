using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;
using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Enums;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.LoadMulticastChatHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StorageS3Shared;

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql.Repository;

public class ChatHistoryRepository : IChatHistoryRepository
{
    private readonly ServiceDbContext _dbContext;
    private readonly ILogger<ChatHistoryRepository> _logger;
    private readonly IS3CommunicationService _s3CommunicationService;

    public ChatHistoryRepository(ServiceDbContext dbContext, ILogger<ChatHistoryRepository> logger, IS3CommunicationService s3CommunicationService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _s3CommunicationService = s3CommunicationService;
    }

    public async Task<LoadChatHistoryResponse[]> LoadUnicastChatHistoryAsync(string firstEmail, string secondEmail)
    {
        var chatHistory= await _dbContext.UnicastChatMessages
            .Where(msg =>
                (msg.FromEmail == firstEmail
                 || msg.FromEmail == secondEmail)
                &&(msg.ToEmail == firstEmail
                   || msg.ToEmail == secondEmail))
            .GroupJoin(
                _dbContext.UnicastFiles,
                msg => msg.Id, // Key from UnicastChatMessages
                file => file.MessageId,     // Key from Files
                (msg, files) => new { msg, files }
            )
            .Select(group => new LoadChatHistoryResponse(
                group.msg.MessageText,
                group.msg.FromEmail,
                group.msg.SendDate,
                group.files.Where(f=>f.MessageId==group.msg.Id)
                    .Select(f=>new FileModel(f.FileName,null, null))
                    .ToArray()))
            .ToArrayAsync();
        return chatHistory;
    }

    public async Task<LoadChatHistoryResponse[]> LoadMulticastChatHistoryAsync(string groupString)
    {
        if (!Enum.TryParse(typeof(Group), groupString, true, out var group))
            throw new ArgumentException($"Не найдено сообщение с группой {groupString}");
        
        var chatHistory = await _dbContext.ChatMessages
            .Where(msg => msg.GroupName == (Group)group)
            .GroupJoin(
                _dbContext.MulticastFiles,
                msg => msg.Id, // Key from ChatMessages
                file => file.MessageId,     // Key from Files
                (msg, files) => new { msg, files }
            )
            .Select(g => new LoadChatHistoryResponse(
                g.msg.MessageText,
                g.msg.SenderEmail,
                g.msg.SendDate,
                g.files.Where(f=>f.MessageId==g.msg.Id)
                    .Select(f=>new FileModel(f.FileName,null, null))
                    .ToArray()
            ))
            .ToArrayAsync();
        return chatHistory;
    }

    private async Task<LoadChatHistoryResponse[]> AddFilesContent(LoadChatHistoryResponse[] chatHistory)
    {
        var fileNames = chatHistory
            .Select(c => c.Files
                .Select(f=>f.FileName).ToArray()).ToArray();
        var files = await _s3CommunicationService.GetFiles(fileNames);
        return chatHistory.Select((c,i)=>new LoadChatHistoryResponse(
            c.MessageText,
            c.SenderEmail,
            c.SentDateTime,
            files[i])).ToArray();
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