using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Payment.ByeProduct;

public class BuyProductCommand:IRequest<string>
{
    public string ProductName { get; set; }
    public Guid UserId { get; set; }
}