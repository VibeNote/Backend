using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Prometheus;
using WebApp.Configuration;

namespace WebApp.Extensions;

public static class StartupExtensions
{
    public static WebApplication Configure(
        this WebApplication app,
        WebAppConfiguration configuration)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseHttpMetrics();
        app.MapMetrics();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapDefaultControllerRoute();
        /*app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=PriceLists}/{id?}");*/
        
        app.MapControllers();
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/openapi.json", "VibeNote");
            c.RoutePrefix = string.Empty;
        });

        var corsOrigin = app.Configuration.GetConnectionString("CorsOrigin")!;
        app.UseCors(o => o.WithOrigins(corsOrigin));

        return app;
    }
}