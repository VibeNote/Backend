using Application.Abstractions.User;
using Contracts.Users.Commands;
using Dto.Common;
using Dto.User;
using Handlers.Users;
using Moq;

namespace Application.Users;

[TestFixture]
[TestOf(typeof(LogoutUserHandler))]
public class LogoutUserHandlerTest
{

    [Test]
    public void HandleTest()
    {
        var mock = new Mock<IUserService>();
        mock.Setup(s => s.Logout(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        
        var handler = new LogoutUserHandler(mock.Object);
        Assert.DoesNotThrowAsync(async () =>
        {
            await handler.Handle(new LogoutUser.Command(Guid.NewGuid(), ""), new CancellationToken());
        });
    }
}