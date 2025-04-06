using Itis.MyTrainings.Api.Core.Abstractions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Payment.ByeProduct;

public class BuyProductCommandHandler(ITransactionClient transactionClient): IRequestHandler<BuyProductCommand, string>
{
    public async Task<string> Handle(BuyProductCommand request, CancellationToken cancellationToken)
    {
        return await transactionClient.ExecuteTransactionAsync(request);
    }
}