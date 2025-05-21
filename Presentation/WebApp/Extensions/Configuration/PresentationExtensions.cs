using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Tools;

namespace WebApp.Extensions.Configuration;

public static class PresentationExtensions
{
    private const int DefaultFormCountLimit = int.MaxValue;

    public static IServiceCollection ConfigurePresentation(this IServiceCollection services)
    {
        services.Configure<FormOptions>(o => o.ValueCountLimit = DefaultFormCountLimit);

        services.AddControllers(o =>
        {
            o.Filters.Add<ModelValidationActionFilter>();
            o.Filters.Add<ExceptionFilter>();
        });

        services.Configure<ApiBehaviorOptions>(o =>
        {
            o.SuppressModelStateInvalidFilter = true;
        });

        services.AddSignalR();

        return services;
    }
}