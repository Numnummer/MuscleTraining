using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Itis.MyTrainings.Api.Web.SignalR;

/// <summary>
/// Хаб для сообщений
/// </summary>
public class NotificationHub : Hub
{
    private IUserService _userService;
    private IDbContext _dbContext;
    
    public NotificationHub(IDbContext dbContext, IUserService userService)
    {
        _userService = userService;
        _dbContext = dbContext;
    }
    
    public async Task SendMessage(string messageText)
    {
        // var coach = await _userService.FindUserByIdAsync(currentUserId);
        //
        // var message = new Message(DateTime.Now, messageText, coach, coach);
        // _dbContext.Messages.Add(message);
        //
        // await Clients.All.SendAsync("ReceieveMessage", message.SendDate,
        //     $"{coach.FirstName} {coach.LastName}", messageText);
        //
        // await _dbContext.SaveChangesAsync();
        
        await Clients.All.SendAsync("ReceiveMessage", DateTime.Now, $"qweqwe qweqweq", messageText);
    }
}