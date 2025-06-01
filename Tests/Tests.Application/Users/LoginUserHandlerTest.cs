using Application.Abstractions.User;
using Contracts.Users.Commands;
using Dto.Common;
using Dto.User;
using Handlers.Users;
using Moq;

namespace Application.Users;

[TestFixture]
[TestOf(typeof(LoginUserHandler))]
public class LoginUserHandlerTest
{

    [Test] public void HandleTest()
    {
        var mock = new Mock<IUserService>();
        mock.Setup(s => s.Login(It.IsAny<CredentialsDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TokenDto("", ""));

        var handler = new LoginUserHandler(mock.Object);
        Assert.DoesNotThrowAsync(async () =>
        {
            await handler.Handle(new LoginUser.Command(new CredentialsDto("", "")), new CancellationToken());
        });
    }
}