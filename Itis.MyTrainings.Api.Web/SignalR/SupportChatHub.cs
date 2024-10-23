using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Itis.MyTrainings.Api.Web.SignalR;

/// <summary>
/// Хаб для чата поддержки
/// </summary>

[Authorize]
public class SupportChatHub : Hub
{
    public async Task SendMessageAsync(string messageText)
    {
        
    }

    public override Task OnConnectedAsync()
    {
        var role=Context.User.Claims.Where(claim=>claim.Type == ClaimTypes.Role).FirstOrDefault();
        return Task.CompletedTask;
    }
}
