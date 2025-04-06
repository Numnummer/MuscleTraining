using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Requests.Payment.ByeProduct;
using Itis.MyTrainings.Api.PostgreSql;
using Itis.MyTrainings.PaymentService.Web.Protos;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Web.Services;

public class TransactionClient(Transaction.TransactionClient client,
    EfContext dbContext):ITransactionClient
{
    public async Task<string> ExecuteTransactionAsync(BuyProductCommand command)
    {
        var product=await dbContext.Products.SingleOrDefaultAsync(p=>p.Name == command.ProductName);
        if (product == null)
        {
            return "Product not found";
        }

        var transactionRequest = new TransactionRequest()
        {
            Amount = product.Price,
            Iteration = 1,
            Remaining = (int)product.Remains,
            UserId = command.UserId.ToString(),
            OperationId = Guid.NewGuid().ToString(),
            ProductId = product.Id.ToString(),
        };
        var result=await client.ExecuteTransactionAsync(transactionRequest);
        return result.Success ? "Commit" : result.Error;
    }
}