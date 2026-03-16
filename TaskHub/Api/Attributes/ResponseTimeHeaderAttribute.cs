using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ResponseTimeHeaderAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        long startedAt = Stopwatch.GetTimestamp();

        context.HttpContext.Response.OnStarting(() =>
        {
            double elapsedMilliseconds = Stopwatch.GetElapsedTime(startedAt).TotalMilliseconds;
            
            context.HttpContext.Response.Headers["X-Response-Time-Ms"] = elapsedMilliseconds
                .ToString(CultureInfo.InvariantCulture);

            return Task.CompletedTask;
        });

        await next();
    }
}