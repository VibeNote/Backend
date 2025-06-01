using Common.Exceptions.ForbiddenExceptions;
using Common.Exceptions.NotFoundExceptions.Entity;
using Contracts.Entry.Queries;
using Core.Entities;
using DataAccess;
using DataAccess.Abstractions;
using Handlers.Entries;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Entries;

[TestFixture]
[TestOf(typeof(GetEntryHandler))]
public class GetEntryHandlerTest
{
    private const string EntryContent = "entryContent";
    private const string User1IdGuid = "a8d5707f-2880-4d22-9561-7d13061b9930";
    private const string User2IdGuid = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string Entry1Id = "f7b844fd-9d26-4603-971f-674bafabac2f";
    private const string Entry2Id = "583b8346-1237-4de6-b014-afaf1812c061";

    [Test]
    public async Task HandleTest()
    {
        var context = await GetMockedContextService();
        var handler = new GetEntryHandler(context);
        
        Assert.DoesNotThrowAsync(async () =>
        {
            var result =  await handler.Handle(
                new GetEntry.Query(Guid.Parse(User1IdGuid), Guid.Parse(Entry1Id)),
                new CancellationToken());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Entry, Is.Not.Null);
        });
    }
    
    [Test]
    public async Task HandleNoEntryTest()
    {
        var context = await GetMockedContextService();
        var handler = new GetEntryHandler(context);
        
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await handler.Handle(
                new GetEntry.Query(Guid.Parse(User1IdGuid), Guid.Parse(Entry2Id)),
                new CancellationToken());
        });
    }
    
    [Test]
    public async Task HandleNoAccessTest()
    {
        var context = await GetMockedContextService();
        var handler = new GetEntryHandler(context);

        Assert.ThrowsAsync<NotEnoughAccessException>(async () =>
        {
            await handler.Handle(
                new GetEntry.Query(Guid.Parse(User2IdGuid), Guid.Parse(Entry1Id)),
                new CancellationToken());
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
            new(Guid.Parse(Entry1Id), Guid.Parse(User1IdGuid), EntryContent, DateTime.UtcNow, DateTime.UtcNow),
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