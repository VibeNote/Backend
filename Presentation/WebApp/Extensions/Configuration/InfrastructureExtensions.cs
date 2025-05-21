using DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Configuration;

namespace WebApp.Extensions.Configuration;

public static class InfrastructureExtensions
{
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        WebAppConfiguration configuration)
    {
        services.AddDatabaseContext(o => o
            .UseNpgsql(
                configuration.PostgreSqlConfiguration
                    .ToConnectionString()
            ));

        return services;
    }
}