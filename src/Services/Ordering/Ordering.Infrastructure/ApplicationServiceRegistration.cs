using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services;
    }
}