using Application.Abstractions.Providers;
using Contracts.Entry.Commands;
using DataAccess;
using DataAccess.Abstractions;
using Dto.Entry;
using Handlers.Entries;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Entries;

[TestFixture]
[TestOf(typeof(SaveEntryHandler))]
public class SaveEntryHandlerTest
{
    private const string EntryContent = "entryContent";
    private const string User1IdGuid = "a8d5707f-2880-4d22-9561-7d13061b9930";

    [Test]
    public async Task HandleTest()
    {
        var context = await GetMockedContextService();
        var dateTimeProvider = GetMockedDateTimeProvider();
        var handler = new SaveEntryHandler(context, dateTimeProvider);
        
        Assert.DoesNotThrowAsync(async () =>
        {
            var result =
                await handler.Handle(
                    new SaveEntry.Command(Guid.Parse(User1IdGuid), new InputEntryDto(EntryContent)),
                    new CancellationToken());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Entry, Is.Not.Null);
            Assert.That(result.Entry.Content, Is.EqualTo(EntryContent));
        });
    }
    
    private static async Task<IVibeNoteDatabaseContext> GetMockedContextService()
    {
        var options = new DbContextOptionsBuilder<VibeNoteDatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new VibeNoteDatabaseContext(options);
        
        var mock = new Mock<IVibeNoteDatabaseContext>();
        mock.SetupGet(c => c.Entries).Returns(dbContext.Entries);
        mock.SetupGet(c => c.Analyses).Returns(dbContext.Analyses);
        mock.SetupGet(c => c.EmotionTags).Returns(dbContext.EmotionTags);
        mock.SetupGet(c => c.Tags).Returns(dbContext.Tags);
        mock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(() => dbContext.SaveChangesAsync());

        return mock.Object;
    }

    private IDateTimeProvider GetMockedDateTimeProvider()
    {
        var mock = new Mock<IDateTimeProvider>();
        mock
            .Setup(s => s.FromUtc(It.IsAny<DateTime>())).Returns(DateTime.Now);

        return mock.Object;
    }
}