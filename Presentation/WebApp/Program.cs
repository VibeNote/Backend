using System.Text;
using DataAccess.Extensions;
using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApp.Configuration;
using WebApp.Extensions;
using WebApp.Extensions.Configuration;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.AddEnvironmentVariables();

var webAppConfiguration = new WebAppConfiguration(configuration);

builder.Services.ConfigureWebApplication(webAppConfiguration);

builder.Services
    .AddMvc(options =>
    {
        options.MaxModelBindingCollectionSize = int.MaxValue;
    });

builder.Services.AddTransient<WebAppConfiguration>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webAppConfiguration.TokenInfoConfiguration.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false
        };
    });

var app = builder.Build().Configure(webAppConfiguration);

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var serviceProvider = scope.ServiceProvider;
    await serviceProvider.UseDatabaseContext();
}

app.Logger.LogInformation("Starting the application");

await app.RunAsync();