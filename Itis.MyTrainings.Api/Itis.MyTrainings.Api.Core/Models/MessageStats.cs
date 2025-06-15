namespace Itis.MyTrainings.Api.Core.Models;

/// <summary>
/// 
/// </summary>
public class MessageStats
{
    public Guid UserId { get; set; }
    public long TotalCount { get; set; }
    public DateTime LastMessageDate { get; set; }
    public DateTime UpdatedAt { get; set; }
}