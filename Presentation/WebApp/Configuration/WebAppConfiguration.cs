using DataAccess.EntityConfiguration;
using Microsoft.Extensions.Configuration;
using WebApp.Extensions.Configuration;

namespace WebApp.Configuration;

public class WebAppConfiguration
{
    public WebAppConfiguration(IConfiguration configuration)
    {
        PostgreSqlConfiguration = configuration.GetSection<PostgreSqlConfiguration>();
    }
    public PostgreSqlConfiguration PostgreSqlConfiguration { get; }
    public TokenInfoConfiguration TokenInfoConfiguration { get; }
}