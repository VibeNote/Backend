using Handlers.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp.Configuration;

namespace WebApp.Extensions.Configuration;

public static class ApplicationExtensions
{
    private const string LoggingTimestampFormat = "[HH:mm:ss dd.MM.yyyy] ";

    public static IServiceCollection ConfigureApplication(
        this IServiceCollection services,
        WebAppConfiguration configuration)
    {
        services.AddLogging(o => o
            .AddSimpleConsole(c => c.TimestampFormat = LoggingTimestampFormat));

        services.AddHandlers();

        return services;
    }
}