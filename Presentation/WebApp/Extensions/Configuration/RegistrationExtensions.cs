using System.Globalization;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
        
        services
            .AddMvc(options =>
            {
                options.MaxModelBindingCollectionSize = int.MaxValue;
            });
        services.AddTransient<WebAppConfiguration>();
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.TokenInfoConfiguration.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                };
            });
        
        return services;
    }
}