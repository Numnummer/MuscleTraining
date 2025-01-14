using StorageS3Shared;

namespace Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;

public interface IS3CommunicationService
{
    Task UploadFiles(string[] fileNames, byte[][] fileBytes, string[] filesMetadata);
    Task DeleteFiles(string[] fileNames);
    Task<FileModel[][]?> GetFiles(string[][] fileNames);
}