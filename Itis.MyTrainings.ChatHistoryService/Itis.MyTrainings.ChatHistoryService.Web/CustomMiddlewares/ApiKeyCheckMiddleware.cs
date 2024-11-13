using System.Net;

namespace Itis.MyTrainings.ChatHistoryService.Web.CustomMiddlewares;

public class ApiKeyCheckMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        string? apiKey = context.Request.Headers["X-API-Key"];
        var validApiKey = Environment.GetEnvironmentVariable("API_KEY");
        if (apiKey == null || apiKey!=validApiKey)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Api key not valid");
            return;
        }
        await _next.Invoke(context);
    }
}