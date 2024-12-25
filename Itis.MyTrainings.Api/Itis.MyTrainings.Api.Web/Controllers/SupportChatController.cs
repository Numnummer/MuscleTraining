using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.Api.Core.Constants;

using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StorageS3Shared;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер для получения истории сообщений из чата поддержки
/// </summary>
public class SupportChatController : BaseController
{
    private readonly IConfiguration _configuration;
    private readonly string? _chatHistoryServiceUrl;
    private readonly ILogger<SupportChatController> _logger;

    public SupportChatController(ILogger<SupportChatController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _chatHistoryServiceUrl = configuration["SupportChatHistoryService:Url"];
    }

    private async Task<LoadChatHistoryResponse[]> LoadChatHistory(string url)
    {
        var client=new HttpClient();
        var apiKey = Environment.GetEnvironmentVariable("API_KEY");
        client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
        var response = await client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        _logger.LogInformation(response.ToString());
        var result = JsonConvert.DeserializeObject<LoadChatHistoryResponse[]>(json);
        return result;
    }

    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("multicast/{group}")]
    public async Task<LoadChatHistoryResponse[]> LoadMulticastChatHistoryAsync(
        string group) =>
        await LoadChatHistory($"{_chatHistoryServiceUrl}/api/ChatHistory/multicast/{group}");
    
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("unicast/{firstEmail}/{secondEmail}")]
    public async Task<LoadChatHistoryResponse[]> LoadUnicastChatHistoryAsync(
        [FromServices] IMediator mediator, string firstEmail, string secondEmail) =>
        await LoadChatHistory($"{_chatHistoryServiceUrl}/api/ChatHistory/unicast/{firstEmail}/{secondEmail}");
    
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("getOneFile/{fileName}")]
    public async Task<FileModel> GetOneFileAsync(string fileName)
    {
        var httpClient = new HttpClient();
        var url = $"{_chatHistoryServiceUrl}/api/ChatHistory/getOneFile/{fileName}";
        return await httpClient.GetFromJsonAsync<FileModel>(url);
    }

}