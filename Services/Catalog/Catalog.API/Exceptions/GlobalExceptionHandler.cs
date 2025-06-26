using FluentValidation;
using Shared.Exceptions;

namespace Catalog.API.Exceptions;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var isDomainException = ex is DomainException || ex is ValidationException;

            context.Response.StatusCode = isDomainException ? 400 : 500;
            context.Response.ContentType = "application/json";

            var errorMessage = isDomainException ? ex.Message : "An unexpected error occurred.";
            await context.Response.WriteAsJsonAsync(errorMessage);
        }
    }
}
