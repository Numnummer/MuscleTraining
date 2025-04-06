using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Requests.Payment.ByeProduct;
using Itis.MyTrainings.Api.PostgreSql;
using Itis.MyTrainings.PaymentService.Web.Protos;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Web.Services;

public class TransactionClient(Transaction.TransactionClient client,
    EfContext dbContext, IDictionary<Guid,Guid> operationsDictionary):ITransactionClient
{
    public async Task<string> ExecuteTransactionAsync(BuyProductCommand command)
    {
        var product=await dbContext.Products.SingleOrDefaultAsync(p=>p.Name == command.ProductName);
        if (product == null)
        {
            return "Product not found";
        }

        if (product.Remains == 0)
        {
            return "There is no remaining product";
        }
        var operationId=Guid.NewGuid();
        operationsDictionary.Add(operationId,product.Id);
        
        var transactionRequest = new TransactionRequest()
        {
            Amount = product.Price,
            Iteration = 1,
            Remaining = (int)product.Remains,
            UserId = command.UserId.ToString(),
            OperationId = operationId.ToString(),
            ProductId = product.Id.ToString(),
        };

        product.Remains--;
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
        
        var result=await client.ExecuteTransactionAsync(transactionRequest);
        operationsDictionary.Remove(operationId);
        return result.Success ? "Commit" : result.Error;
    }
}