using Microsoft.Extensions.Configuration;
using WebApp.Extensions.Configuration;

namespace WebApp.Configuration;

public class WebAppConfiguration
{
    public WebAppConfiguration(IConfiguration configuration)
    {
        PostgreSqlConfiguration = configuration.GetSection<PostgreSqlConfiguration>();
        TokenInfoConfiguration = configuration.GetSection<TokenInfoConfiguration>();
        ContainersConfiguration = configuration.GetSection<ContainersConfiguration>();
    }
    public PostgreSqlConfiguration PostgreSqlConfiguration { get; }
    public TokenInfoConfiguration TokenInfoConfiguration { get; }
    public ContainersConfiguration ContainersConfiguration { get; }
}