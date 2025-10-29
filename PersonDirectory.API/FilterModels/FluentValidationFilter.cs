using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using PersonDirectory.API.Localization;

public class FluentValidationFilter : IActionFilter
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStringLocalizer<ValidationMessages> _localizer;

    public FluentValidationFilter(IServiceProvider serviceProvider, IStringLocalizer<ValidationMessages> localizer)
    {
        _serviceProvider = serviceProvider;
        _localizer = localizer;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument == null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = _serviceProvider.GetService(validatorType) as IValidator;

            if (validator == null) continue;

            var validationContext = new ValidationContext<object>(argument);
            var result = validator.Validate(validationContext);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                            ? _localizer["InvalidValue"]
                            : e.ErrorMessage).ToArray()
                    );

                context.Result = new BadRequestObjectResult(new
                {
                    message = _localizer["ValidationFailed"],
                    errors
                });

                return;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
