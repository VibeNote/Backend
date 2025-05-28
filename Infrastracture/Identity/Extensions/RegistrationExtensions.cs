using Application.Abstractions.User;
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
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<ITokenGenerator, TokenGenerator>(s => new TokenGenerator(getSecret(s), getIssuer(s)))
            .AddScoped<IUserService, UserService>();

        return services;
    }
}