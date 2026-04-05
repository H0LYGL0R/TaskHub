using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class StudentInfoHeadersFilter : IAsyncResultFilter
{
    private const string StudentName = "Medyantsev Svyatoslav Victorovich";
    private const string StudentGroup = "RI-240943";

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        context.HttpContext.Response.Headers["X-Student-Name"] = StudentName;
        context.HttpContext.Response.Headers["X-Student-Group"] = StudentGroup;

        await next();
    }
}