using Application.Abstractions.Analysis;
using Common.Enums;
using Common.Exceptions.ForbiddenExceptions;
using Common.Exceptions.PreconditionFailedExceptions;
using Common.Extentions;
using Contracts.Analysis.Commands;
using Core.Entities;
using DataAccess;
using DataAccess.Abstractions;
using Dto.Tag;
using Handlers.Analysis;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Analysis;

[TestFixture]
[TestOf(typeof(AnalyseEntryHandler))]
public class AnalyseEntryHandlerTest
{
    private const string EntryContent = "entryContent";
    private const string User1IdGuid = "a8d5707f-2880-4d22-9561-7d13061b9930";
    private const string User2IdGuid = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string Entry1Id = "f7b844fd-9d26-4603-971f-674bafabac2f";
    private const string Entry2Id = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string Result = "Hi";
    
    
    [Test]
    public async Task HandleTest()
    {
        var analysisService = GetMockedAnalysisService();
        var context = await GetMockedContextService();

        var handler = new AnalyseEntryHandler(context, analysisService);
        
        Assert.DoesNotThrowAsync(async () =>
        {
            var result = await handler.Handle(new AnalyseEntry.Command(Guid.Parse(User1IdGuid), Guid.Parse(Entry1Id)),
                new CancellationToken());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Analysis, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Analysis.Result, Is.EqualTo(Result));
                Assert.That(result.Analysis.EntryText, Is.EqualTo(EntryContent));
                Assert.That(result.Analysis.Tags, Has.Count.EqualTo(2));
            });
        });
    }

    [Test]
    public async Task HandleNotEnoughAccessTest()
    {
        var analysisService = GetMockedAnalysisService();
        var context = await GetMockedContextService();

        var handler = new AnalyseEntryHandler(context, analysisService);

        Assert.ThrowsAsync<NotEnoughAccessException>(async () =>
        {
            await handler.Handle(new AnalyseEntry.Command(Guid.Parse(User2IdGuid), Guid.Parse(Entry1Id)),
                new CancellationToken());
        });
    }
    
    [Test]
    public async Task HandleEntityAlreadyHasAnalysisTest()
    {
        var analysisService = GetMockedAnalysisService();
        var context = await GetMockedContextService();

        var handler = new AnalyseEntryHandler(context, analysisService);

        Assert.ThrowsAsync<EntryLogicException>(async () =>
        {
            await handler.Handle(new AnalyseEntry.Command(Guid.Parse(User1IdGuid), Guid.Parse(Entry2Id)),
                new CancellationToken());
        });
    }

    private static IAnalysisService GetMockedAnalysisService()
    {
        var mock = new Mock<IAnalysisService>();
        var fakeTags = new List<AnalysedTagDto>() { new(TagsEnum.Joy, 0.5), new(TagsEnum.Anger, 0.4) };
        mock
            .Setup(s => s.GetResultAsync(EntryContent, fakeTags, It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                "Hi"
            );
        mock
            .Setup(s => s.GetContentTagsAsync(EntryContent, It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                fakeTags
            );

        return mock.Object;
    }

    private static async Task<IVibeNoteDatabaseContext> GetMockedContextService()
    {
        var options = new DbContextOptionsBuilder<VibeNoteDatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new VibeNoteDatabaseContext(options);
        
        var entries = new List<Entry>
        {
            new Entry(Guid.Parse(Entry1Id), Guid.Parse(User1IdGuid), EntryContent, DateTime.UtcNow, DateTime.UtcNow),
            new Entry(Guid.Parse(Entry2Id), Guid.Parse(User1IdGuid), EntryContent, DateTime.UtcNow, DateTime.UtcNow),
        };
        var analyses = new List<Core.Entities.Analysis>
        {
            new Core.Entities.Analysis(Guid.NewGuid(),Guid.Parse(Entry2Id), Result, DateTime.UtcNow)
        };
        var tags = new List<Tag>
        {
            new(Guid.NewGuid(), TagsEnum.Joy, TagsEnum.Joy.ToRuName(), TagsEnum.Joy.ToEngName()),
            new(Guid.NewGuid(), TagsEnum.Anger, TagsEnum.Anger.ToRuName(), TagsEnum.Anger.ToEngName())
        };
        
        await dbContext.Entries.AddRangeAsync(entries);
        await dbContext.Tags.AddRangeAsync(tags);
        await dbContext.Analyses.AddRangeAsync(analyses);
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