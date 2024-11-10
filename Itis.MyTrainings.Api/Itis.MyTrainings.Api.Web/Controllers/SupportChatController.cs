using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.Api.Core.Constants;

using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер для получения истории сообщений из чата поддержки
/// </summary>
public class SupportChatController : BaseController
{
    private readonly string _chatHistoryServiceUrl="http://chat-history-service:80";
    private readonly ILogger<SupportChatController> _logger;

    public SupportChatController(ILogger<SupportChatController> logger)
    {
        _logger = logger;
    }

    private async Task<LoadChatHistoryResponse[]> LoadChatHistory(string url)
    {
        var client=new HttpClient();
        var response = await client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        _logger.LogInformation(response.ToString());
        return JsonConvert.DeserializeObject<LoadChatHistoryResponse[]>(json);
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

}