using Grpc.Core;
using Itis.MyTrainings.PaymentService.Web.Protos;

namespace Itis.MyTrainings.Api.Web.GrpcServices;

public class RevertTransactionService:RevertTransaction.RevertTransactionBase
{
    public override Task<RevertTransactionResponse> RevertTransaction(RevertTransactionRequest request, ServerCallContext context)
    {
        return base.RevertTransaction(request, context);
    }
}