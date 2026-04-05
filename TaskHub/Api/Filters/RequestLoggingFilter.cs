using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class RequestLoggingFilter(ILogger<RequestLoggingFilter> logger) : IAsyncActionFilter
{
    private readonly ILogger<RequestLoggingFilter> _logger = logger;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string method = context.HttpContext.Request.Method;
        string path = context.HttpContext.Request.Path;

        _logger.LogInformation("Начало выполнения экшена: {Method} {Path}", method, path);

        long startedAt = Stopwatch.GetTimestamp();
        ActionExecutedContext executedContext = await next();
        double elapsedMs = Stopwatch.GetElapsedTime(startedAt).TotalMilliseconds;

        int statusCode = executedContext.HttpContext.Response.StatusCode;
        _logger.LogInformation(
            "Завершение выполнения экшена: status={StatusCode}, elapsedMs={ElapsedMs:F2}",
            statusCode,
            elapsedMs);
    }
}