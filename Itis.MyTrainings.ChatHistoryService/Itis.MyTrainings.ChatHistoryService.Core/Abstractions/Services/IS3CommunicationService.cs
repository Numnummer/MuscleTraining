namespace Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;

public interface IS3CommunicationService
{
    Task UploadFiles(string[] fileNames, byte[][] fileBytes);
    Task DeleteFiles(string[] fileNames);
}