using Api.Controllers.Tasks.Requests;
using Api.Tasks.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class ValidateSetTaskTitleRequestFilter : IActionFilter
{
    private const string RequestMissingMessage = "Тело запроса отсутствует";

    public void OnActionExecuting(ActionExecutingContext context)
    {
        SetTaskTitleRequest? request = MakeRequest(context);
        if (request is null)
        {
            context.Result = new BadRequestObjectResult(RequestMissingMessage);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    private static SetTaskTitleRequest? MakeRequest(ActionExecutingContext context)
    {
        SetTaskTitleRequest? request =
            
            context
                .ActionArguments.Values
                .OfType<SetTaskTitleRequest?>()
                .FirstOrDefault();
        
        return request;
    }
}