using Microsoft.Extensions.Configuration;

namespace WebApp.Extensions.Configuration;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Gets section based on the name of generic parameter
    /// </summary>
    public static T GetSection<T>(this IConfiguration configuration)
    {
        var configurationName = typeof(T).Name;

        var section = configuration
            .GetSection(configurationName)
            .Get<T>();

        /* Think about using configuration existence checks
        if (section is null)
            throw new Exception($"Section with name {configurationName} is not defined");
        */

        return section!;
    }
}