using Microsoft.Extensions.DependencyInjection;

namespace Handlers.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediator(o => o.ServiceLifetime = ServiceLifetime.Transient);

        return services;
    }
}