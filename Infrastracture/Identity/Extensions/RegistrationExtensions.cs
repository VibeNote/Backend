using Core.Entities;
using Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services,
        Func<IServiceProvider, string> getSecret,
        Func<IServiceProvider, string> getIssuer)
    {
        services
            .AddScoped<PasswordHasherWrapper>()
            .AddScoped(s => new TokenGenerator(getSecret(s), getIssuer(s)))
            .AddScoped<UserService>();

        return services;
    }
}