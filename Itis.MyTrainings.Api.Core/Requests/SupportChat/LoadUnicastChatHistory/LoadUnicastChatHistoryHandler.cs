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
        return await _dbContext.UnicastChatMessages
            .Where(msg =>
                (msg.FromEmail == request.FirstEmail
                 || msg.FromEmail == request.SecondEmail)
                &&(msg.ToEmail == request.FirstEmail 
                   || msg.ToEmail == request.SecondEmail))
            .Select(msg => new LoadChatHistoryResponse(msg.MessageText, msg.FromEmail, msg.SendDate))
            .ToArrayAsync(cancellationToken);
    }
}