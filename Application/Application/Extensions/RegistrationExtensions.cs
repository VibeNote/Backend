using Application.Abstractions.Providers;
using Application.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddRussianServices(this IServiceCollection services)
    {
        services
            .AddTransient<IDateTimeProvider, MskDateTimeProvider>();
        return services;
    }
}