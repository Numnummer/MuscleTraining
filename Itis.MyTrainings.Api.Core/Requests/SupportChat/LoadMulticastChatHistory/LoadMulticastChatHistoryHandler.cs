using System.ComponentModel;
using System.Reflection.Metadata;
using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.SupportChat;

public class LoadMulticastChatHistoryHandler : 
    IRequestHandler<LoadMulticastChatHistoryQuery,LoadChatHistoryResponse[]>
{
    private readonly IDbContext _dbContext;

    public LoadMulticastChatHistoryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LoadChatHistoryResponse[]> Handle(LoadMulticastChatHistoryQuery request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(typeof(Group), request.Group, true, out var group))
            throw new InvalidEnumArgumentException($"Не найдено сообщение с группой {request.Group}");
        
        return await _dbContext.ChatMessages
            .Where(msg => msg.GroupName == (Group)group)
            .Select(msg=>
                new LoadChatHistoryResponse(msg.MessageText,msg.SenderEmail, msg.SendDate))
            .ToArrayAsync(cancellationToken);
    }
}