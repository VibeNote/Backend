namespace Application.Abstractions.User;

public interface IPasswordHasher
{
    string GenerateHash(string password);
    bool VerifyPassword(string userPassword, string password);
}