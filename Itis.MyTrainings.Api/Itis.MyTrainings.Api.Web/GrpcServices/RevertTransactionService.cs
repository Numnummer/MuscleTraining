using Grpc.Core;
using Itis.MyTrainings.Api.PostgreSql;
using Itis.MyTrainings.PaymentService.Web.Protos;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Web.GrpcServices;

public class RevertTransactionService(IDictionary<Guid,Guid> operationsDictionary,
    EfContext dbContext):
    RevertTransaction.RevertTransactionBase
{
    public override async Task<RevertTransactionResponse> RevertTransaction(RevertTransactionRequest request, ServerCallContext context)
    {
        var product =
            await dbContext.Products.SingleOrDefaultAsync(p =>
                p.Id == operationsDictionary[Guid.Parse(request.OperationId)]);
        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found."));
        }

        product.Remains++;
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
        
        operationsDictionary.Remove(Guid.Parse(request.OperationId));
        return new RevertTransactionResponse { Success = true};
    }
}