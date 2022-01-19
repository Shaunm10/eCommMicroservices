using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviors;

/// <summary>
/// This Pipeline Behavior does the logging of any exception that is thrown in a MediatR Handler.
/// </summary>
public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            // try the next piece in the pipeline.
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            this._logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
            throw;
        }
    }
}