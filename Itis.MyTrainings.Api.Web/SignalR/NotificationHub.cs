using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Itis.MyTrainings.Api.Web.SignalR;

/// <summary>
/// Хаб для сообщений
/// </summary>
[EnableCors]
public class NotificationHub : Hub<INotificationClient>
{
    private IUserService _userService;
    private IDbContext _dbContext;
    
    public NotificationHub(IDbContext dbContext, IUserService userService)
    {
        _userService = userService;
        _dbContext = dbContext;
    }
    
    public async Task SendMessage(string messageText, Guid currentUserId)
    {
        var coach = await _userService.FindUserByIdAsync(currentUserId)
            ?? throw new ApplicationExceptionBase($"Не найден тренер с id: {currentUserId}");
        
        var message = new Message(DateTime.Now, messageText, coach.Id);
        _dbContext.Messages.Add(message);
        
        await Clients.All.ReceiveMessage(
            new NotificationModel(DateTime.Now, $"{coach.FirstName} {coach.LastName}", messageText));
        
        await _dbContext.SaveChangesAsync();
    }
}

public interface INotificationClient
{
    public Task ReceiveMessage(NotificationModel model);
}

public record NotificationModel(DateTime CreatedDate, string From, string Message);
