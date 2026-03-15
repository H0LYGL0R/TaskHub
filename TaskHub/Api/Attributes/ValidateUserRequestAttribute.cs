using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class ValidateUserRequestAttribute : Attribute, IActionFilter
{
    private const string RequestMissingMessage = "Тело запроса отсутствует";
    private const string NameMissingMessage = "Имя пользователя не задано";
    private const string NameProperty = "Name";

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.Count == 0)
        {
            context.Result = new BadRequestObjectResult(RequestMissingMessage);
            return;
        }
        
        object? request = context.ActionArguments.Values.FirstOrDefault();

        if (request is null)
        {
            context.Result = new BadRequestObjectResult(RequestMissingMessage);
            return;
        }
        
        PropertyInfo? nameProperty = request.GetType().GetProperty(NameProperty);
        if (nameProperty == null) return;

        string? name = nameProperty.GetValue(request) as string;

        if (string.IsNullOrWhiteSpace(name))
        {
            context.Result = new BadRequestObjectResult(NameMissingMessage);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}