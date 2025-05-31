using DataAccess.Extensions;
using Identity.Extensions;
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
                    .ToConnectionString(configuration.ContainersConfiguration.PostgresName)
            ));

        services
            .AddIdentityServices(
                s => configuration.TokenInfoConfiguration.Secret,
                s => configuration.TokenInfoConfiguration.Issuer
                );

        return services;
    }
}