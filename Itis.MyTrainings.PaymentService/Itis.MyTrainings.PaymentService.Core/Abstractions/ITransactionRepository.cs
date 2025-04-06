using Itis.MyTrainings.PaymentService.Core.Entities;

namespace Itis.MyTrainings.PaymentService.Core.Abstractions;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
}