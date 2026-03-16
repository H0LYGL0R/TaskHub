using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class StudentInfoHeadersAttribute : Attribute, IAsyncActionFilter
{
    private const string StudentName = "Medyantsev Svyatoslav Victorovich";
    private const string StudentGroup = "RI-240943";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Response.OnStarting(() =>
        {
            context.HttpContext.Response.Headers["X-Student-Name"] = StudentName;
            context.HttpContext.Response.Headers["X-Student-Group"] = StudentGroup;

            return Task.CompletedTask;
        });

        await next();
    }
}