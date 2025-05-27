using System.Security.Cryptography;
using Application.Abstractions.User;

namespace Identity.Services;

public class PasswordHasherWrapper: IPasswordHasherWrapper
{

    public PasswordHasherWrapper()
    {
    }

    public string GenerateHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(32);
        
        var result = new byte[1 + salt.Length + hash.Length];
        result[0] = 0x01;
        Buffer.BlockCopy(salt, 0, result, 1, salt.Length);
        Buffer.BlockCopy(hash, 0, result, 1 + salt.Length, hash.Length);

        return Convert.ToBase64String(result);
    }

    public bool VerifyPassword(string userPassword, string password)
    {
        var decoded = Convert.FromBase64String(userPassword);

        if (decoded[0] != 0x01)
            throw new NotSupportedException("Unknown hash version");

        var salt = new byte[16];
        var hash = new byte[32];

        Buffer.BlockCopy(decoded, 1, salt, 0, 16);
        Buffer.BlockCopy(decoded, 17, hash, 0, 32);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        var hashToCompare = pbkdf2.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
    }
}