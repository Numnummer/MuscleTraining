using Itis.MyTrainings.Api.Core.Models;

namespace Itis.MyTrainings.Api.Core.Abstractions;

public interface IMessageStatsService
{
    Task<MessageStats> ProcessMessageAsync(Guid userId);
}