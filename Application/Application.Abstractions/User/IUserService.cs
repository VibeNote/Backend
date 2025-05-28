using Dto.Common;
using Dto.User;

namespace Application.Abstractions.User;

public interface IUserService
{
    Task<TokenDto> Login(CredentialsDto credentials, CancellationToken cancellationToken);
    Task<TokenDto> Register(RegisterUserDto registerUser, CancellationToken cancellationToken);
    Task Logout(Guid userId, string token, CancellationToken cancellationToken);
    Task<TokenDto> GetToken(Guid userId, string token, CancellationToken cancellationToken);
    bool IsExists(Guid userId);
}