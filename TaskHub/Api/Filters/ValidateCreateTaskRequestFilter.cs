using Api.Controllers.Tasks.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class ValidateCreateTaskRequestFilter : IActionFilter
{
    private const string RequestMissingMessage = "Тело запроса отсутствует";
    private const string UserIdMissingMessage = "Идентификатор пользователя не задан";
    private const string TitleMissingMessage = "Название задачи не задано";

    public void OnActionExecuting(ActionExecutingContext context)
    {
        CreateTaskRequest? request = context.ActionArguments.Values.OfType<CreateTaskRequest?>().FirstOrDefault();
        if (request is null)
        {
            context.Result = new BadRequestObjectResult(RequestMissingMessage);
            return;
        }

        if (request.CreatedByUserId == Guid.Empty)
        {
            context.Result = new BadRequestObjectResult(UserIdMissingMessage);
            return;
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            context.Result = new BadRequestObjectResult(TitleMissingMessage);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}