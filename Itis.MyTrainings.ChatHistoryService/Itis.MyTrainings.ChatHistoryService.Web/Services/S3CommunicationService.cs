using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.S3Communication;
using Microsoft.Extensions.Options;
using StorageS3Shared;

namespace Itis.MyTrainings.ChatHistoryService.Web.Services;

public class S3CommunicationService : IS3CommunicationService
{
    private readonly IOptionsMonitor<StorageServiceOptions> _storageOptions;

    public S3CommunicationService(IOptionsMonitor<StorageServiceOptions> storageOptions)
    {
        _storageOptions = storageOptions;
    }

    public async Task UploadFiles(string[] fileNames, byte[][] fileBytes)
    {
        var httpClient = new HttpClient();
        var body = fileNames.
            Select((fileName, index) => 
                new FileModel(fileName, fileBytes[index]))
            .ToArray();
        var url = $"{_storageOptions.CurrentValue.Url}/putFile";
        var res = await httpClient.PutAsJsonAsync(url, body);
        res.EnsureSuccessStatusCode();
    }

    public async Task DeleteFiles(string[] fileNames)
    {
        var httpClient = new HttpClient();
        var url = $"{_storageOptions.CurrentValue.Url}/deleteFile";
        var res = await httpClient.PutAsJsonAsync(url, fileNames);
        res.EnsureSuccessStatusCode();
    }
}