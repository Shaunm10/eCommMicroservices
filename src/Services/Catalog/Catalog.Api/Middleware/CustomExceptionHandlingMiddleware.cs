using System.Net;

namespace Catalog.Api.Middleware;

public class CustomExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlingMiddleware> _logger;

    public CustomExceptionHandlingMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlingMiddleware> logger)
    {
        this._next = next;
        this._logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception ex)
        {
            await this.HandleGlobalException(context, ex);
        }
    }

    private Task HandleGlobalException(HttpContext context, Exception ex)
    {
        if (ex is ApplicationException)
        {
            this._logger.LogWarning($"Validaton error occured in Api. {ex.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsJsonAsync(new { ex.Message });
        }
        else
        {
            var errorId = Guid.NewGuid();
            this._logger.LogError(ex, $"Error occured in Api:{errorId}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsJsonAsync(new
            {
                ErrorId = errorId,
                Message = "Something bad happend in our Api." +
                "Contact our support team with the ErrorId if the issue persists."
            });
        }
    }
}
