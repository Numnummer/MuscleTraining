using Itis.MyTrainings.Api.Contracts.Requests.SupportChat.LoadMulticastChatHistory;
using Itis.MyTrainings.Api.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.SupportChat.LoadUnicastChatHistory;

public class LoadUnicastChatHistoryHandler : IRequestHandler<LoadUnicastChatHistoryQuery, LoadChatHistoryResponse[]>
{
    private readonly IDbContext _dbContext;

    public LoadUnicastChatHistoryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<LoadChatHistoryResponse[]> Handle(LoadUnicastChatHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.ChatMessages
            .Where(msg => msg.SenderEmail == request.Email)
            .Select(msg => new LoadChatHistoryResponse(msg.MessageText, msg.SenderEmail, msg.SendDate))
            .ToArrayAsync(cancellationToken);
    }
}