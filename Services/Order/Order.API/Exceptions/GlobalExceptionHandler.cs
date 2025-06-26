using Shared.Exceptions;

namespace Order.API.Exceptions;

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
            context.Response.StatusCode = ex is DomainException ? 400 : 500;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Message = ex is DomainException ? ex.Message : "An unexpected error occurred.",
                Details = ex.Message
            };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
