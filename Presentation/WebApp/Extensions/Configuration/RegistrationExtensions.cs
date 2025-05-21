using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Configuration;

namespace WebApp.Extensions.Configuration;

public static class RegistrationExtensions
{
    public static IServiceCollection ConfigureWebApplication(
        this IServiceCollection services,
        WebAppConfiguration configuration)
    {
        
        services
            .ConfigureInfrastructure(configuration)
            .ConfigureApplication(configuration)
            .ConfigurePresentation();

        return services;
    }
}