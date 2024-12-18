using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;
using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Itis.MyTrainings.ChatHistoryService.PostgreSql;

namespace Itis.MyTrainings.ChatHistoryService.Web.Services;

public class ChatHistoryRecordService : IChatHistoryRecordService
{
    private readonly IChatHistoryRepository _chatHistoryRepository;
    private readonly IFilesRepository _filesRepository;
    private readonly IS3CommunicationService _s3CommunicationService;
    private readonly ServiceDbContext _dbContext;
    private readonly ILogger<ChatHistoryRecordService> _logger;

    public ChatHistoryRecordService(IChatHistoryRepository chatHistoryRepository, IFilesRepository filesRepository, IS3CommunicationService s3CommunicationService, ServiceDbContext dbContext, ILogger<ChatHistoryRecordService> logger)
    {
        _chatHistoryRepository = chatHistoryRepository;
        _filesRepository = filesRepository;
        _s3CommunicationService = s3CommunicationService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task RecordMessage(ChatMessage message, string[] fileNames, byte[][] filesContent)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _s3CommunicationService.UploadFiles(fileNames, filesContent);
            await _filesRepository.AddMulticastFileAsync(fileNames, message.Id);
            await _chatHistoryRepository.RecordMessageAsync(message);
            await transaction.CommitAsync();
            _logger.LogInformation($"Recording done");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, "Error while recording message");
            await transaction.RollbackAsync();
            try
            {
                await _s3CommunicationService.DeleteFiles(fileNames);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, "Error while delete file");
                throw;
            }
            throw;
        }
    }

    public async Task RecordUnicastMessage(UnicastChatMessage message, string[] fileNames, byte[][] filesContent)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _s3CommunicationService.UploadFiles(fileNames, filesContent);
            await _filesRepository.AddUnicastFileAsync(fileNames, message.Id);
            await _chatHistoryRepository.RecordUnicastMessageAsync(message);
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, "Error while recording message");
            await transaction.RollbackAsync();
            try
            {
                await _s3CommunicationService.DeleteFiles(fileNames);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, "Error while delete file");
                throw;
            }
            throw;
        }
    }
}