using Itis.MyTrainings.Api.Core.Requests.Payment.ByeProduct;

namespace Itis.MyTrainings.Api.Core.Abstractions;

public interface ITransactionClient
{
    Task<string> ExecuteTransactionAsync(BuyProductCommand command);
}