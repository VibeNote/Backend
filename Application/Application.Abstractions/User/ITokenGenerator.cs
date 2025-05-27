namespace Application.Abstractions.User;

public interface ITokenGenerator
{
    string GenerateToken(Guid userId);
}