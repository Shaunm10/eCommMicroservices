using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviors;

namespace Ordering.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper extension
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // FluentValidator extension
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // MediatR extension
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // MediatR pipeline behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}