using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Requests.Payment.ByeProduct;
using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

[Authorize]
public class PaymentController : BaseController
{
    /// <summary>
    /// Провести тразакцию с оплатой
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpPost("processPayment")]
    public async Task<string> ProcessPayment([FromServices] IMediator mediator,
        [FromBody] string productName)
    => await mediator.Send(new BuyProductCommand { ProductName = productName, UserId = CurrentUserId});
}