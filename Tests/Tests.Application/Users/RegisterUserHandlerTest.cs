using Application.Abstractions.User;
using Contracts.Users.Commands;
using Dto.Common;
using Dto.User;
using Handlers.Users;
using Moq;

namespace Application.Users;

[TestFixture]
[TestOf(typeof(RegisterUserHandler))]
public class RegisterUserHandlerTest
{

    [Test]
    public void HandleTest()
    {
        var mock = new Mock<IUserService>();
        mock.Setup(s => s.Register(It.IsAny<RegisterUserDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(new TokenDto("", ""));
        
        var handler = new RegisterUserHandler(mock.Object);
        Assert.DoesNotThrowAsync(async () =>
        {
            await handler.Handle(new RegisterUser.Command(new RegisterUserDto("", new CredentialsDto("", ""))), new CancellationToken());
        });
    }
}