using Application.Abstractions.Analysis;
using Application.Abstractions.Providers;
using Application.Analysis;
using Application.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddProviders(
        this IServiceCollection services)
    {
        services
            .AddTransient<IDateTimeProvider, MskDateTimeProvider>();
        return services;
    }
    
    public static IServiceCollection AddAnalysisService(
        this IServiceCollection services,
        Func<IServiceProvider, HttpClient> getEmotionsClient,
        Func<IServiceProvider, HttpClient> getRecommendationsClient)
    {
        services
            .AddScoped<IAnalysisService>(s =>
                new AnalysisService(
                    getEmotionsClient(s),
                    getRecommendationsClient(s)
                )
            );
        return services;
    }
}