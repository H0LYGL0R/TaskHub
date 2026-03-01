namespace Api.Middlewares;

public sealed class StudentInfoMiddleware
{
    private const string StudentName = "Medyantesev Svyatoslav Victorovich";
    private const string StudentGroup = "RI-240943";

    private readonly RequestDelegate _next;

    public StudentInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-Student-Name"] = StudentName;
            context.Response.Headers["X-Student-Group"] = StudentGroup;

            return Task.CompletedTask;
        });

        await _next(context);
    }
}