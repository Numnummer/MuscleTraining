
namespace Itis.MyTrainings.PaymentService.Core.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateTime { get; set; }
    public uint Iteration  { get; set; }
    public uint ProductCountRemains { get; set; }
}