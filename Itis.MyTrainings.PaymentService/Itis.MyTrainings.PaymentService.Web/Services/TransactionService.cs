using Grpc.Core;
using Itis.MyTrainings.PaymentService.Core.Abstractions;
using Itis.MyTrainings.PaymentService.Web.Protos;

namespace Itis.MyTrainings.PaymentService.Web.Services;

public class TransactionService(ITransactionRepository repository, 
    RevertTransaction.RevertTransactionClient client,
    ILogger<TransactionService> logger): Transaction.TransactionBase
{
    public override async Task<TransactionResponse> ExecuteTransaction(TransactionRequest request, ServerCallContext context)
    {
        var transaction = new Core.Entities.Transaction()
        {
            Id = Guid.NewGuid(),
            Iteration = (uint)request.Iteration,
            DateTime = DateTime.UtcNow,
            ProductCountRemains = (uint)request.Remaining,
            ProductId = Guid.Parse(request.ProductId),
            UserId = Guid.Parse(request.UserId),
        };
        try
        {
            logger.LogInformation($"Executing transaction {transaction.Id}. Paying {request.Amount}$");
            await repository.AddAsync(transaction);
            return new TransactionResponse()
            {
                TransactionId = transaction.Id.ToString(),
                Success = true
            };
        }
        catch (Exception e)
        {
            var revertRequest = new RevertTransactionRequest()
            {
                OperationId = request.OperationId,
            };
            var result=await client.RevertTransactionAsync(revertRequest);
            return new TransactionResponse()
            {
                TransactionId = transaction.Id.ToString(),
                Error = result.Error,
                Success = false
            };
        }
        
    }

}