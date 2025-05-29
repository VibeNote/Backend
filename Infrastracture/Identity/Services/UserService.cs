using Application.Abstractions.Providers;
using Application.Abstractions.User;
using Common.Exceptions.BadRequestExceptions.User;
using Common.Exceptions.NotFoundExceptions.User;
using Common.Exceptions.UnauthorizedExceptions;
using Core.Entities;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Extensions;
using Dto.Common;
using Dto.User;

namespace Identity.Services;

public class UserService: IUserService
{
    private readonly IVibeNoteDatabaseContext _context;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserService(IVibeNoteDatabaseContext context, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<TokenDto> Login(CredentialsDto credentials, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindByLogin(credentials.Login);
        if (user == null)
        {
            throw UserNotFoundException.InvalidLogin();
        }
        
        if (!_passwordHasher.VerifyPassword(user.PasswordHash, credentials.Password))
        {
            throw UserServiceException.InvalidCredentials();
        }

        var token = _tokenGenerator.GenerateToken(user.Id);
        var tokenEntity = new Token(Guid.NewGuid(), user.Id, token);
        await _context.Tokens.AddAsync(tokenEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new TokenDto(token, user.UserName);
    }

    public async Task<TokenDto> Register(RegisterUserDto registerUser, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindByLogin(registerUser.Credentials.Login);
        if (user != null)
        {
            throw UserServiceException.UserNameIsNotUnique(registerUser.Credentials.Login);
        }

        var passwordHash = _passwordHasher.GenerateHash(registerUser.Credentials.Password);
        user = new User(
            Guid.NewGuid(), 
            registerUser.Username, 
            registerUser.Credentials.Login,
            passwordHash,
            DateTime.UtcNow);
        
        var token = _tokenGenerator.GenerateToken(user.Id);
        var tokenEntity = new Token(Guid.NewGuid(), user.Id, token);
        
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.Tokens.AddAsync(tokenEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new TokenDto(token, registerUser.Username);
    }

    public async Task Logout(Guid userId, string token, CancellationToken cancellationToken)
    {
        var tokenEntity = await _context.Tokens.FindByTokenAndUserIdAsync(userId, token);
        if (tokenEntity == null)
        {
            return;
        }

        _context.Tokens.Remove(tokenEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TokenDto> GetToken(Guid userId, string token, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindByIdAsync(userId, cancellationToken: cancellationToken);
        if (user == null)
        {
            throw TokenValidatorException.IsUnauthorized();
        }

        var tokenEntity = await _context
            .Tokens
            .FindByTokenAndUserIdAsync(userId, token);

        if (tokenEntity == null)
        {
            throw TokenValidatorException.InvalidToken(userId, token);
        }

        return new TokenDto(tokenEntity.Value, user.UserName);
    }

    public bool IsExists(Guid userId)
    {
        var user = _context.Users.FindById(userId);
        return user != null;
    }
}