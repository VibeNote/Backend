using Application.Abstractions.Providers;
using Application.Abstractions.User;
using Common.Enums;
using Common.Exceptions.BadRequestExceptions.User;
using Common.Exceptions.NotFoundExceptions.User;
using Common.Exceptions.UnauthorizedExceptions;
using Common.Extentions;
using Core.Entities;
using DataAccess;
using DataAccess.Abstractions;
using Dto.User;
using Identity.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastracture.Services;

[TestFixture]
[TestOf(typeof(UserService))]
public class UserServiceTest
{
    
    private const string User1Id = "a8d5707f-2880-4d22-9561-7d13061b9930";
    private const string User2Id = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string User1Login = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string User2Login = "a8d5707f-2880-4d22-9561-7d13061b9930";
    private const string UserName = "aaa";
    private const string Token1 = "583b8346-1237-4de6-b014-afaf1812c061";
    private const string Token2 = "a8d5707f-2880-4d22-9561-7d13061b9930";
    private const string Password = "ANNPN";
    private const string PasswordHash = "ANNPN";
    
    [Test]
    public async Task GetTokenTest()
    {
        var context = await GetMockedContextService();
        var service = new UserService(context, null!, null!);
        Assert.DoesNotThrowAsync(async () =>
        {
            var token = await service.GetToken(Guid.Parse(User1Id), Token1, new CancellationToken());
            Assert.That(token, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(token.Username, Is.EqualTo(UserName));
                Assert.That(token.AccessToken, Is.EqualTo(Token1));
            });
        });
    }
    
    [Test]
    public async Task GetTokenUserNotFoundTest()
    {
        var context = await GetMockedContextService();
        var service = new UserService(context, null!, null!);
        Assert.ThrowsAsync<TokenValidatorException>(async () =>
        {
            await service.GetToken(Guid.Parse(User2Id), Token1, new CancellationToken());
        });
    }
    
    [Test]
    public async Task GetTokenNotFoundTest()
    {
        var context = await GetMockedContextService();
        var service = new UserService(context, null!, null!);
        Assert.ThrowsAsync<TokenValidatorException>(async () =>
        {
            await service.GetToken(Guid.Parse(User1Id), Token2, new CancellationToken());
        });
    }
    
    [Test]
    public async Task IsExistsTrueTest()
    {
        var context = await GetMockedContextService();
        var service = new UserService(context, null!, null!);
        Assert.DoesNotThrow(() =>
        {
            var result = service.IsExists(Guid.Parse(User1Id));
            Assert.That(result, Is.EqualTo(true));
        });
    }
    
    [Test]
    public async Task IsExistsFalseTest()
    {
        var context = await GetMockedContextService();
        var service = new UserService(context, null!, null!);
        Assert.DoesNotThrow(() =>
        {
            var result = service.IsExists(Guid.Parse(User2Id));
            Assert.That(result, Is.EqualTo(false));
        });
    }
    
    [Test]
    public async Task LogoutTest()
    {
        var context = await GetMockedContextService();
        var service = new UserService(context, null!, null!);
        
        Assert.DoesNotThrowAsync(async () =>
        {
            await service.Logout(Guid.Parse(User1Id), Token1, new CancellationToken());
        });
    }
    
    [Test]
    public async Task RegisterTest()
    {
        var context = await GetMockedContextService();
        var tokenGenerator = GetMockedTokenGenerator();
        var passwordHasher = GetMockedPasswordHasher(true);

        var service = new UserService(context, tokenGenerator, passwordHasher);
        Assert.DoesNotThrowAsync(async () =>
        {
            var token = await service.Register(
                new RegisterUserDto(UserName, new CredentialsDto(User2Login, Password)),
                new CancellationToken());
            Assert.That(token, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(token.Username, Is.EqualTo(UserName));
                Assert.That(token.AccessToken, Is.EqualTo(Token1));
            });
        });
    }
    
    [Test]
    public async Task RegisterUsernameNotUniqueTest()
    {
        var context = await GetMockedContextService();
        var tokenGenerator = GetMockedTokenGenerator();
        var passwordHasher = GetMockedPasswordHasher(true);

        var service = new UserService(context, tokenGenerator, passwordHasher);

        Assert.ThrowsAsync<UserServiceException>(async () =>
        {
            await service.Register(
                new RegisterUserDto(UserName, new CredentialsDto(User1Login, Password)),
                new CancellationToken());
        });
    }
    
    [Test]
    public async Task LoginTest()
    {
        var context = await GetMockedContextService();
        var tokenGenerator = GetMockedTokenGenerator();
        var passwordHasher = GetMockedPasswordHasher(true);

        var service = new UserService(context, tokenGenerator, passwordHasher);
        Assert.DoesNotThrowAsync(async () =>
        {
            await service.Login(new CredentialsDto(User1Login, Password), new CancellationToken());
        });
    }
    
    [Test]
    public async Task LoginNotFoundTest()
    {
        var context = await GetMockedContextService();
        var tokenGenerator = GetMockedTokenGenerator();
        var passwordHasher = GetMockedPasswordHasher(true);

        var service = new UserService(context, tokenGenerator, passwordHasher);
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
        {
            await service.Login(new CredentialsDto(User2Login, Password), new CancellationToken());
        });
    }
    
    [Test]
    public async Task LoginInvalidPasswordTest()
    {
        var context = await GetMockedContextService();
        var tokenGenerator = GetMockedTokenGenerator();
        var passwordHasher = GetMockedPasswordHasher(false);

        var service = new UserService(context, tokenGenerator, passwordHasher);
        Assert.ThrowsAsync<UserServiceException>(async () =>
        {
            await service.Login(new CredentialsDto(User1Login, Password), new CancellationToken());
        });
    }


    private static ITokenGenerator GetMockedTokenGenerator()
    {
        var mock = new Mock<ITokenGenerator>();
        mock
            .Setup(s => s.GenerateToken(It.IsAny<Guid>())).Returns(Token1);

        return mock.Object;
    }

    private static IPasswordHasher GetMockedPasswordHasher(bool equalsResult)
    {
        var mock = new Mock<IPasswordHasher>();
        mock
            .Setup(s => s.GenerateHash(It.IsAny<string>())).Returns(PasswordHash);

        mock
            .Setup(s => s.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(equalsResult);

        return mock.Object;
    }
    
    
    private static async Task<IVibeNoteDatabaseContext> GetMockedContextService()
    {
        var options = new DbContextOptionsBuilder<VibeNoteDatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new VibeNoteDatabaseContext(options);
        
        var tokens = new List<Token>
        {
            new Token(Guid.NewGuid(), Guid.Parse(User1Id), Token1),
        };
        var users = new List<User>
        {
            new User(Guid.Parse(User1Id), UserName, User1Login, PasswordHash, DateTime.UtcNow)
        };
        
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.Tokens.AddRangeAsync(tokens);
        await dbContext.SaveChangesAsync();
        
        var mock = new Mock<IVibeNoteDatabaseContext>();
        mock.SetupGet(c => c.Users).Returns(dbContext.Users);
        mock.SetupGet(c => c.Tokens).Returns(dbContext.Tokens);
        mock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(() => dbContext.SaveChangesAsync());

        return mock.Object;
    }
}