using Microsoft.AspNetCore.SignalR;

namespace Itis.MyTrainings.Api.Web.SignalR.Filters;

public class HubFilter : IHubFilter
{
    public async ValueTask<object> InvokeMethodAsync(
        HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
    {
        return await next(invocationContext);
    }
}