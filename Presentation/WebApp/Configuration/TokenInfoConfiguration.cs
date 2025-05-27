namespace WebApp.Configuration;

public class TokenInfoConfiguration
{
    public required string Secret { get; init; }    
    public required string Issuer { get; init; }
    
}