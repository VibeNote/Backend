using Core.Entities;
using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class VibeNoteDatabaseContext(DbContextOptions<VibeNoteDatabaseContext> options)
    : DbContext(options), IVibeNoteDatabaseContext
{
    public DbSet<User> Users { get; init; }
    public DbSet<Token> Tokens { get; init; }
    public DbSet<Entry> Entries { get; init; }
    public DbSet<Analysis> Analyses { get; init; }
    public DbSet<EmotionTag> EmotionTags { get; init; }
    public DbSet<Tag> Tags { get; init; }
    public DbSet<TriggerWord> TriggerWords { get; init; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<decimal>()
            .HavePrecision(18, 6);

        base.ConfigureConventions(configurationBuilder);
    }
}