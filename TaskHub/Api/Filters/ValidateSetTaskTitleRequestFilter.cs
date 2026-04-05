using Api.Tasks.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class ValidateSetTaskTitleRequestFilter : IActionFilter
{
    private const string RequestMissingMessage = "Тело запроса отсутствует";

    public void OnActionExecuting(ActionExecutingContext context)
    {
        SetTaskTitleRequest? request = context.ActionArguments.Values.OfType<SetTaskTitleRequest?>().FirstOrDefault();
        if (request is null)
        {
            context.Result = new BadRequestObjectResult(RequestMissingMessage);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}