﻿namespace WebApp.Configuration;

public class PostgreSqlConfiguration
{
    private static string Polling => "Pooling=true;MinPoolSize=1;MaxPoolSize=75;";
    
    public required string Host { get; init; }
    public required int Port { get; init; }

    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string DatabaseName { get; init; }

    public string ToConnectionString(string containerName)
    {
        return $"Host={containerName};Port={Port};Database={DatabaseName};Username={Username};Password={Password};{Polling}";
    }    public string ToConnectionString()
    {
        return $"Host={Host};Port={Port};Database={DatabaseName};Username={Username};Password={Password};{Polling}";
    }
}