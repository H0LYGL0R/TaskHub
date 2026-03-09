using System.Diagnostics;

namespace Api.Middlewares;

public sealed class ResponseTimeMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseTimeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        long startedAt = Stopwatch.GetTimestamp();

        context.Response.OnStarting(() => SetResponseTimeHeader(context, startedAt));

        await _next(context);
    }

    private static Task SetResponseTimeHeader(HttpContext context, long startedAt)
    {
        double elapsedMilliseconds = Stopwatch.GetElapsedTime(startedAt).TotalMilliseconds;

        context.Response.Headers["X-Response-Time-Ms"] = elapsedMilliseconds.ToString();

        return Task.CompletedTask;
    }
}
