using System.Globalization;
using FloraPlanet.WebApp.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WebApp.Configuration;

namespace FloraPlanet.WebApp.Extensions;

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

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapDefaultControllerRoute();
        /*app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=PriceLists}/{id?}");*/
        app.MapRazorPages();
        
        app.MapControllers();

        string corsOrigin = app.Configuration.GetConnectionString("CorsOrigin")!;
        app.UseCors(o => o.WithOrigins(corsOrigin));

        return app;
    }
}