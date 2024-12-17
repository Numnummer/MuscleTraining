using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql.Repository;

public class FilesRepository : IFilesRepository
{
    private readonly ServiceDbContext _dbContext;

    public FilesRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUnicastFileAsync(string[] fileNames, Guid messageId)
    {
        var files = fileNames
            .Select(fileName => new Files
            {
                FileName = fileName,
                Id = Guid.NewGuid(),
                MessageId = messageId
            })
            .ToArray();
        await _dbContext.UnicastFiles.AddRangeAsync(files);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddMulticastFileAsync(string[] fileNames, Guid messageId)
    {
        var files = fileNames
            .Select(fileName => new Files
            {
                FileName = fileName,
                Id = Guid.NewGuid(),
                MessageId = messageId
            })
            .ToArray();
        await _dbContext.MulticastFiles.AddRangeAsync(files);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUnicastFileAsync(string fileName)
    {
        var file=await _dbContext.UnicastFiles.FirstOrDefaultAsync(f=>f.FileName == fileName);
        _dbContext.Remove(file);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMulticastFileAsync(string fileName)
    {
        var file=await _dbContext.MulticastFiles.FirstOrDefaultAsync(f=>f.FileName == fileName);
        _dbContext.Remove(file);
        await _dbContext.SaveChangesAsync();
    }
}