using Grpc.Core;
using Grpc.Core.Interceptors;
using Npgsql;

namespace Discount.Grpc.Middleware
{
    public class LoggerInterceptor : Interceptor
    {
        private readonly ILogger<LoggerInterceptor> _logger;

        public LoggerInterceptor(ILogger<LoggerInterceptor> logger)
        {
            this._logger = logger;
        }

        public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            this.LogCall(context);
            try
            {
                this._logger.LogInformation($"incoming call to: {context.Method}");
                return await base.UnaryServerHandler(request, context, continuation);
            }
            catch (NpgsqlException sqlEx)
            {
                this._logger.LogError(sqlEx, $"An Postgres error occurred when calling {context.Method}");
                throw new RpcException(new Status(StatusCode.Internal, "Sql Error"));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"A error occurred when calling {context.Method}");
                throw new RpcException(new Status(StatusCode.Internal, "Exception"));
            }
        }

        private void LogCall(ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            this._logger.LogDebug($"Starting call. Request: {httpContext.Request.Path}");
        }
    }
}
