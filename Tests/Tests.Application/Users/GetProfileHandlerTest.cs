using Common.Exceptions.NotFoundExceptions.Entity;
using Contracts.Users.Queries;
using Core.Entities;
using DataAccess;
using DataAccess.Abstractions;
using Handlers.Users;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Users;

[TestFixture]
[TestOf(typeof(GetProfileHandler))]
public class GetProfileHandlerTest
{
    private const string User1Id = "a8d5707f-2880-4d22-9561-7d13061b9930";
    private const string User2Id = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string UserLogin = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string UserName = "aaa";
    private const string PasswordHash = "ANNPN";

    [Test] public async Task HandleTest()
    {
        var context = await GetMockedContextService();
        var handler = new GetProfileHandler(context);
        
        Assert.DoesNotThrowAsync(async () =>
        {
            var response = await handler.Handle(new GetProfile.Query(Guid.Parse(User1Id)), new CancellationToken());
            Assert.That(response, Is.Not.Null);
            Assert.That(response.User, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.User.UserId, Is.EqualTo(Guid.Parse(User1Id)));
                Assert.That(response.User.Login, Is.EqualTo(UserLogin));
                Assert.That(response.User.Username, Is.EqualTo(UserName));
            });
        });
    }
    
    [Test] public async Task HandleNotFoundTest()
    {
        var context = await GetMockedContextService();
        var handler = new GetProfileHandler(context);
        
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await handler.Handle(new GetProfile.Query(Guid.Parse(User2Id)), new CancellationToken());
        });
    }
    
    private static async Task<IVibeNoteDatabaseContext> GetMockedContextService()
    {
        var options = new DbContextOptionsBuilder<VibeNoteDatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new VibeNoteDatabaseContext(options);
        
        var users = new List<User>
        {
            new User(Guid.Parse(User1Id), UserName, UserLogin, PasswordHash, DateTime.UtcNow)
        };
        
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
        
        var mock = new Mock<IVibeNoteDatabaseContext>();
        mock.SetupGet(c => c.Users).Returns(dbContext.Users);
        mock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(() => dbContext.SaveChangesAsync());

        return mock.Object;
    }
}