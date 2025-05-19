using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Abstractions;

public interface IVibeNoteDatabaseContext
{
    DbSet<User> Users { get; }
    DbSet<Token> Tokens { get; }
    DbSet<Entry> Entries { get; }
    DbSet<Analysis> Analyses { get; }
    DbSet<EmotionTag> EmotionTags { get; }
    DbSet<Tag> Tags { get; }
    DbSet<TriggerWord> TriggerWords { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}