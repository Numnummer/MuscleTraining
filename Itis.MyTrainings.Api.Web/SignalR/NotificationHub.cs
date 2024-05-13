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

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, Context.User!.Identity!.Name!);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User!.Identity!.Name!);
        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task SendMessage(string messageText, string recieverUsername)
    {
        var coachId = await _userService.GetCurrentUserId();

        var coach = await _userService.FindUserByIdAsync(coachId);

        var reciever = await _userService.FindUserByUserNameAsync(recieverUsername);

        var message = new Message(DateTime.Now, messageText, coach, reciever);
        _dbContext.Messages.Add(message);

        await Clients.Group(reciever.Id.ToString()).SendAsync("RecieveMessage", message.SendDate,
            $"{coach.FirstName} {coach.LastName}", messageText);

        await _dbContext.SaveChangesAsync();
    }
}