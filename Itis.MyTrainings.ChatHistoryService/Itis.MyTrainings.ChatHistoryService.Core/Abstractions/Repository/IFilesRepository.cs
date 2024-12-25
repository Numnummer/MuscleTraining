namespace Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Repository;

public interface IFilesRepository
{
    Task AddUnicastFileAsync(string[] fileName, Guid messageId);
    Task AddMulticastFileAsync(string[] fileName, Guid messageId);
    Task DeleteUnicastFileAsync(string fileName);
    Task DeleteMulticastFileAsync(string fileName);
}