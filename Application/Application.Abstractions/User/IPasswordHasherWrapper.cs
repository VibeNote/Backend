namespace Application.Abstractions.User;

public interface IPasswordHasherWrapper
{
    string GenerateHash(string password);
    bool VerifyPassword(string userPassword, string password);
}