using Microsoft.Extensions.Localization;
using PersonDirectory.API.Localization;
using PersonDirectory.Application.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IStringLocalizer<ValidationMessages> _localizer;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IStringLocalizer<ValidationMessages> localizer)
    {
        _next = next;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status404NotFound, _localizer[$"{ex.ValidationKey}", ex.Key], ex);
        }
        catch (AlreadyExsistExeption ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, _localizer["AlreadyExsists", ex.Key], ex);
        }
        catch (BadRequestExeption ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, _localizer[$"{ex.ReasonKey}"], ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, _localizer["UnexpectedError"], ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message, Exception ex)
    {
        _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = message,
            detail = ex.Message
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}