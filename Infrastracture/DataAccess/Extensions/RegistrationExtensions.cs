using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddDatabaseContext(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> options)
    {
        services.AddDbContext<IVibeNoteDatabaseContext, VibeNoteDatabaseContext>(options);
        return services;
    }

    public static async Task UseDatabaseContext(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<VibeNoteDatabaseContext>();
        await context.Database.MigrateAsync();
    }
}