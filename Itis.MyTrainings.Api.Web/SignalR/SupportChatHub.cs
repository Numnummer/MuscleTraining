using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Itis.MyTrainings.Api.Web.SignalR;

/// <summary>
/// Хаб для чата поддержки
/// </summary>

public class SupportChatHub : Hub
{
    private readonly string _usersRoom = "usersRoom";
    private readonly string _coachesRoom = "coachesRoom";
    private readonly string _adminsRoom = "adminsRoom";
    
    // In-memory dictionary to store connectionId and email
    private static readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();
    
    /// <summary>
    /// Отправить групповое сообщение
    /// </summary>
    /// <param name="author"></param>
    /// <param name="messageText"></param>
    /// <param name="destination"></param>
    public async Task SendMulticastMessageAsync(string author, string messageText, string destination)
    {
        var date = DateTime.Now;
        switch (destination)
        {
            case "Пользователь":
                await Clients.Group(_usersRoom).SendAsync("onMessageReceive", author, messageText, date);
                break;
            case "Тренер":
                await Clients.Group(_coachesRoom).SendAsync("onMessageReceive", author, messageText, date);
                break;
            case "Админ":
                await Clients.Group(_adminsRoom).SendAsync("onMessageReceive", author, messageText, date);
                break;
        }
    }
    
    /// <summary>
    /// Отправить личное сообщение
    /// </summary>
    /// <param name="messageText"></param>
    /// <param name="destination"></param>
    public async Task SendUnicastMessageAsync(string author ,string messageText, string email)
    {
        var connectionId = _connections.FirstOrDefault(x => x.Value == email).Key;
        await Clients.Client(connectionId)
            .SendAsync("onMessageReceive", author, messageText, DateTime.Now);
    }

    /// <summary>
    /// Добавить пользователя в группу по его роли 
    /// </summary>
    /// <param name="roleName"></param>
    public async Task AddToGroupByRoleAsync(string roleName)
    {
        switch (roleName)
        {
            case "User":
                await Groups.AddToGroupAsync(Context.ConnectionId, _usersRoom);
                break;
            case "Coach":
                await Groups.AddToGroupAsync(Context.ConnectionId, _coachesRoom);
                break;
            case "Administrator":
                await Groups.AddToGroupAsync(Context.ConnectionId, _adminsRoom);
                break;
        }
    }

    /// <summary>
    /// Добавить группу с участником с данным email
    /// </summary>
    /// <param name="email"></param>
    public void AddEmailAsync(string email)
    {
        _connections.TryAdd(Context.ConnectionId, email);
    }

    /// <summary>
    /// Вызывается при отключении клиента
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connections.TryRemove(Context.ConnectionId, out _);
        return base.OnDisconnectedAsync(exception);
    }
}
