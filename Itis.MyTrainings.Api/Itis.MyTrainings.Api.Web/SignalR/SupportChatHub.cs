using System.Collections.Concurrent;
using System.Security.Claims;
using AutoMapper;
using ChatMessageDtos;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Group = Itis.MyTrainings.Api.Core.Entities.SupportChat.Group;

namespace Itis.MyTrainings.Api.Web.SignalR;

/// <summary>
/// Хаб для чата поддержки
/// </summary>

public class SupportChatHub : Hub
{
    private readonly string _userCoachRoom = "userCoachRoom";
    private readonly string _userAdminRoom = "userAdminRoom";
    private readonly string _coachAdminRoom = "coachAdminRoom";

    private readonly IBus _bus;
    private readonly IMapper _mapper;

    public SupportChatHub(IBus bus, IMapper mapper)
    {
        _bus = bus;
        _mapper = mapper;
    }

    // In-memory dictionary to store connectionId and email
    private static readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();
    
    /// <summary>
    /// Отправить групповое сообщение
    /// </summary>
    /// <param name="author"></param>
    /// <param name="messageText"></param>
    /// <param name="destination"></param>
    public async Task SendMulticastMessageAsync(string author, string messageText, 
        string destination, string role, string[] fileNames, byte[][] filesContent)
    {
        var date = DateTime.Now;
        var group = new Group();
        switch (destination + " " + role)
        {
            case "Пользователь Coach":
                group = Group.UserCoach;
                await Clients.Group(_userCoachRoom).SendAsync("onMulticastMessageReceive", author, messageText, date, group.ToString());
                break;
            case "Пользователь Administrator":
                group = Group.UserAdmin;
                await Clients.Group(_userAdminRoom).SendAsync("onMulticastMessageReceive", author, messageText, date, group.ToString());
                break;
            case "Тренер User":
                group = Group.UserCoach;  
                await Clients.Group(_userCoachRoom).SendAsync("onMulticastMessageReceive", author, messageText, date, group.ToString());
                break;
            case "Тренер Administrator":
                group = Group.CoachAdmin;  
                await Clients.Group(_coachAdminRoom).SendAsync("onMulticastMessageReceive", author, messageText, date, group.ToString());
                break;
            case "Админ User":
                group = Group.UserAdmin;
                await Clients.Group(_userAdminRoom).SendAsync("onMulticastMessageReceive", author, messageText, date, group.ToString());
                break;
            case "Админ Coach":
                group = Group.CoachAdmin;
                await Clients.Group(_coachAdminRoom).SendAsync("onMulticastMessageReceive", author, messageText, date, group.ToString());
                break;
        }

        var message = new ChatMessage()
        {
            GroupName = group,
            MessageText = messageText,
            Id = Guid.NewGuid(),
            SendDate = date,
            SenderEmail = author,
        };
        var dto = _mapper.Map<MulticastChatMessageDto>(message);
        dto.FileNames = fileNames;
        dto.FilesContent = filesContent;
        await _bus.Publish(dto);
    }

    /// <summary>
    /// Отправить личное сообщение
    /// </summary>
    /// <param name="author"></param>
    /// <param name="messageText"></param>
    /// <param name="destination"></param>
    public async Task SendUnicastMessageAsync(string author, string messageText, 
        string destination, string[] fileNames, byte[][] filesContent)
    {
        var date = DateTime.Now;
        var connectionId = _connections.FirstOrDefault(x => x.Value == destination).Key;
        await Clients.Client(connectionId)
            .SendAsync("onUnicastMessageReceive", author, messageText, date);
        
        var message = new UnicastChatMessage()
        {
            FromEmail = author,
            MessageText = messageText,
            Id = new Guid(),
            SendDate = date,
            ToEmail = destination,
        };
        var dto = _mapper.Map<UnicastChatMessageDto>(message);
        dto.FileNames = fileNames;
        dto.FilesContent = filesContent;
        await _bus.Publish(dto);
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
                await Groups.AddToGroupAsync(Context.ConnectionId, _userCoachRoom);
                await Groups.AddToGroupAsync(Context.ConnectionId, _userAdminRoom);
                break;
            case "Coach":
                await Groups.AddToGroupAsync(Context.ConnectionId, _userCoachRoom);
                await Groups.AddToGroupAsync(Context.ConnectionId, _coachAdminRoom);
                break;
            case "Administrator":
                await Groups.AddToGroupAsync(Context.ConnectionId, _userAdminRoom);
                await Groups.AddToGroupAsync(Context.ConnectionId, _coachAdminRoom);
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
