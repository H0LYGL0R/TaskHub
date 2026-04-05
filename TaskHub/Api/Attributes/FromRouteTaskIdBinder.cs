using Microsoft.AspNetCore.Mvc.ModelBinding;

public sealed class FromRouteTaskIdBinder : IModelBinder
{
    private const string MissingTaskIdError = "Идентификатор задачи не задан";
    private const string InvalidTaskIdFormatError = "Идентификатор задачи имеет некорректный формат";

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        ValueProviderResult valueProviderResult = GetValueFromRoute(bindingContext);
        if (IsTaskIdMissing(valueProviderResult))
        {
            SetFailedResult(bindingContext, MissingTaskIdError);
            return Task.CompletedTask;
        }

        if (!TryParseTaskId(valueProviderResult, out Guid taskId))
        {
            SetFailedResult(bindingContext, InvalidTaskIdFormatError);
            return Task.CompletedTask;
        }

        SetSuccessResult(bindingContext, taskId);
        return Task.CompletedTask;
    }

    private static ValueProviderResult GetValueFromRoute(ModelBindingContext bindingContext)
    {
        return bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
    }

    private static bool IsTaskIdMissing(ValueProviderResult valueProviderResult)
    {
        return valueProviderResult == ValueProviderResult.None
            || string.IsNullOrWhiteSpace(valueProviderResult.FirstValue);
    }

    private static bool TryParseTaskId(ValueProviderResult valueProviderResult, out Guid taskId)
    {
        return Guid.TryParse(valueProviderResult.FirstValue, out taskId);
    }

    private static void SetFailedResult(ModelBindingContext bindingContext, string errorMessage)
    {
        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, errorMessage);
        bindingContext.Result = ModelBindingResult.Failed();
    }

    private static void SetSuccessResult(ModelBindingContext bindingContext, Guid taskId)
    {
        bindingContext.Result = ModelBindingResult.Success(taskId);
    }
}
