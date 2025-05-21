using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApp.Tools;

public class ModelValidationActionFilter : IActionFilter
{
    private const string ErrorMessagesSeparator = ", ";

    private readonly ILogger<ModelValidationActionFilter> _logger;

    public ModelValidationActionFilter(
        ILogger<ModelValidationActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var modelState = context.ModelState;
        if (modelState.IsValid)
            return;

        var errorMessages = modelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        LogErrorMessages(context, errorMessages);

        context.Result = new BadRequestObjectResult(errorMessages);
    }

    public void OnActionExecuted(ActionExecutedContext context) { }

    private void LogErrorMessages(ActionExecutingContext context, IReadOnlyCollection<string> errorMessages)
    {
        var actionName = context.ActionDescriptor.DisplayName;

        var errorMessage = string.Join(ErrorMessagesSeparator, errorMessages);

        _logger.LogWarning("Failed validation on {action}: {error}", actionName, errorMessage);
    }
}