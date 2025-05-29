using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.User;

namespace Identity.Services;

public class PasswordHasher: IPasswordHasher
{
    public string GenerateHash(string password)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(inputBytes);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string userPassword, string password)
    {
        var actualHash = GenerateHash(password);
        var expectedBytes = Convert.FromBase64String(userPassword);
        var actualBytes = Convert.FromBase64String(actualHash);
        
        return CryptographicOperations.FixedTimeEquals(
            expectedBytes,
            actualBytes
        );
    }
}