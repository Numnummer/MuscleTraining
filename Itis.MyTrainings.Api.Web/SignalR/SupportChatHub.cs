using System.Collections.Concurrent;
using System.Security.Claims;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using MassTransit;
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

    private readonly IBus _bus;

    public SupportChatHub(IBus bus)
    {
        _bus = bus;
    }

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
        var group = new Group();
        switch (destination)
        {
            case "Пользователь":
                group = Group.Users;
                await Clients.Group(_usersRoom).SendAsync("onMessageReceive", author, messageText, date);
                break;
            case "Тренер":
                group = Group.Coaches;  
                await Clients.Group(_coachesRoom).SendAsync("onMessageReceive", author, messageText, date);
                break;
            case "Админ":
                group = Group.Admins;
                await Clients.Group(_adminsRoom).SendAsync("onMessageReceive", author, messageText, date);
                break;
        }

        var message = new ChatMessage()
        {
            GroupName = group,
            MessageText = messageText,
            Id = new Guid(),
            SendDate = date,
            SenderEmail = author,
        };
        await _bus.Publish(message);
    }
    
    /// <summary>
    /// Отправить личное сообщение
    /// </summary>
    /// <param name="messageText"></param>
    /// <param name="destination"></param>
    public async Task SendUnicastMessageAsync(string author ,string messageText, string email)
    {
        var date = DateTime.Now;
        var connectionId = _connections.FirstOrDefault(x => x.Value == email).Key;
        await Clients.Client(connectionId)
            .SendAsync("onMessageReceive", author, messageText, date);
        
        var message = new ChatMessage()
        {
            GroupName = null,
            MessageText = messageText,
            Id = new Guid(),
            SendDate = date,
            SenderEmail = author,
        };
        await _bus.Publish(message);
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
