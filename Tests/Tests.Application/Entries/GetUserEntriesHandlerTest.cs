using Contracts.Entry.Queries;
using Core.Entities;
using DataAccess;
using DataAccess.Abstractions;
using Handlers.Entries;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Entries;

[TestFixture]
[TestOf(typeof(GetUserEntriesHandler))]
public class GetUserEntriesHandlerTest
{
    private const string EntryContent = "entryContent";
    private const string User1IdGuid = "a8d5707f-2880-4d22-9561-7d13061b9930";
    private const string User2IdGuid = "583b8346-1237-4de6-b014-afaf1812c061";

    [Test]
    public async Task HandleHasEntriesTest()
    {
        var context = await GetMockedContextService();
        var handler = new GetUserEntriesHandler(context);
        
        Assert.DoesNotThrowAsync(async () =>
        {
            var result =  await handler.Handle(
                new GetUserEntries.Query(Guid.Parse(User1IdGuid)),
                new CancellationToken());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Entries, Has.Count.EqualTo(2));
        });
    }
    
    [Test]
    public async Task HandleHasNoEntriesTest()
    {
        var context = await GetMockedContextService();
        var handler = new GetUserEntriesHandler(context);
        
        Assert.DoesNotThrowAsync(async () =>
        {
            var result =  await handler.Handle(
                new GetUserEntries.Query(Guid.Parse(User2IdGuid)),
                new CancellationToken());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Entries, Is.Empty);
        });
    }
    
    private static async Task<IVibeNoteDatabaseContext> GetMockedContextService()
    {
        var options = new DbContextOptionsBuilder<VibeNoteDatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new VibeNoteDatabaseContext(options);
        
        var entries = new List<Entry>
        {
            new(Guid.NewGuid(), Guid.Parse(User1IdGuid), EntryContent, DateTime.UtcNow, DateTime.UtcNow),
            new(Guid.NewGuid(), Guid.Parse(User1IdGuid), EntryContent, DateTime.UtcNow, DateTime.UtcNow),
        };
        
        await dbContext.Entries.AddRangeAsync(entries);
        await dbContext.SaveChangesAsync();
        
        var mock = new Mock<IVibeNoteDatabaseContext>();
        mock.SetupGet(c => c.Entries).Returns(dbContext.Entries);
        mock.SetupGet(c => c.Analyses).Returns(dbContext.Analyses);
        mock.SetupGet(c => c.EmotionTags).Returns(dbContext.EmotionTags);
        mock.SetupGet(c => c.Tags).Returns(dbContext.Tags);
        mock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(() => dbContext.SaveChangesAsync());

        return mock.Object;
    }
}