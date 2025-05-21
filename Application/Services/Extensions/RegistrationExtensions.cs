using Microsoft.Extensions.DependencyInjection;

namespace Services.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddApplicationConfiguration(
        this IServiceCollection services)
    {
        return services;
    }
}