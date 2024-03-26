using System.Net;
using System.Text.Json;
using Itis.MyTrainings.Api.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;

namespace Itis.MyTrainings.Api.Web.Middlewares;

/// <summary>
/// Обработчик ошибок API
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="next">Делегат следующего обработчика в пайплайне ASP.NET Core</param>
    /// <param name="loggerFactory">Фабрика логгеров</param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _loggerFactory = loggerFactory;
    }

    /// <summary>
    /// Действия по обработке запроса
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleNotFoundExceptionAsync(context, ex);
        }
        catch (ApplicationExceptionBase ex)
        {
            await HandleApplicationExceptionBaseAsync(context, ex);
        }
        catch (OutOfMemoryException ex)
        {
            await HandleOutOfMemoryExceptionAsync(context, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleUnauthorizedAccessExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    /// <summary>
    /// Создать логгер для контроллера, исполнявшего запрос
    /// </summary>
    /// <param name="context">Контекст запроса</param>
    /// <returns>Логгер</returns>
    private ILogger GetLogger(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var controllerActionDescriptor = endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>();
        var controllerType = controllerActionDescriptor?.ControllerTypeInfo?.AsType();
        return controllerType != null
            ? _loggerFactory.CreateLogger(controllerType)
            : _loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
    }
    
    private async Task LogAndReturnAsync(
        HttpContext context,
        Exception exception,
        string errorText,
        HttpStatusCode responseCode,
        LogLevel logLevel,
        Dictionary<string, object>? details = null)
    {
        details ??= new Dictionary<string, object>();

        GetLogger(context).Log(logLevel, exception, "Error #{errorId}: {exception}");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)responseCode;

        var response = new ProblemDetails
        {
            Title = errorText,
            Instance = null,
            Status = (int)responseCode,
            Type = null,
        };

        foreach (var detail in details)
            response.Extensions.Add(detail.Key, detail.Value);

        var jsonOptions = context.RequestServices.GetRequiredService<IOptions<JsonOptions>>();
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response, jsonOptions.Value.JsonSerializerOptions));
    }

    /// <summary>
    /// Обработка исключения <see cref="NotFoundException"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
    {
        var errorText = exception.Message;
        var logLevel = LogLevel.Warning;
        var responseCode = HttpStatusCode.NotFound;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }

    /// <summary>
    /// Обработка исключения <see cref="ApplicationExceptionBase"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleApplicationExceptionBaseAsync(HttpContext context, ApplicationExceptionBase exception)
    {
        var errorText = exception.Message;
        var logLevel = LogLevel.Warning;
        var responseCode = HttpStatusCode.BadRequest;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }

    /// <summary>
    /// Обработка исключения <see cref="OutOfMemoryException"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleOutOfMemoryExceptionAsync(HttpContext context, OutOfMemoryException exception)
    {
        var errorText =
            $"Server has troubles with its infrastructure. Please inform system administrator about this issue.";
        var logLevel = LogLevel.Error;
        var responseCode = HttpStatusCode.InternalServerError;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }

    /// <summary>
    /// Обработка исключения <see cref="UnauthorizedAccessException"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleUnauthorizedAccessExceptionAsync(HttpContext context, Exception exception)
    {
        var errorText = "Доступ к запрашиваемому ресурсу ограничен";
        var logLevel = LogLevel.Warning;
        var responseCode = HttpStatusCode.Forbidden;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }

    /// <summary>
    /// Обработка исключения <see cref="Exception"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorText = exception.Message;
        var logLevel = LogLevel.Error;
        var responseCode = HttpStatusCode.InternalServerError;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }
}