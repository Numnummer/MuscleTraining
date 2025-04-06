using Itis.MyTrainings.PaymentService.Core.Abstractions;
using Itis.MyTrainings.PaymentService.Core.Entities;

namespace Itis.MyTrainings.PaymentService.DataAccess.Repository;

public class TransactionRepository(EfContext context):ITransactionRepository
{
    public async Task AddAsync(Transaction transaction)
    {
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();
    }
}