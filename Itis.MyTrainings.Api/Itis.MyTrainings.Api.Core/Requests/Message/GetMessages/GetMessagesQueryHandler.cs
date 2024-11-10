using Itis.MyTrainings.Api.Contracts.Requests.Message.GetMessages;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.Message.GetMessages;

/// <summary>
/// Обработчик запроса для <see cref="GetMessagesQuery"/>
/// </summary>
public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, GetMessagesResponse>
{
    private IDbContext _dbContext;
    private IUserService _userService;

    /// <summary>
    /// Конструктор 
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userService"></param>
    public GetMessagesQueryHandler(IDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }
    
    /// <inheritdoc />
    public async Task<GetMessagesResponse> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var _ = await _userService.FindUserByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        var result = await _dbContext.Messages
            .Select(x => new GetMessagesResponseItem()
            {
                MesssageText = x.MessageText,
                SendDate = x.SendDate,
                SenderName = $"{x.Sender.FirstName} {x.Sender.LastName}"
            })
            .ToListAsync(cancellationToken);
        
        return new GetMessagesResponse(result);
    }
}