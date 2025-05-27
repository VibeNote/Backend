namespace Identity.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenGenerator
{
    private readonly string _key;
    private readonly string _issuer;

    public TokenGenerator(string key, string issuer)
    {
        _key = key;
        _issuer = issuer;
    }

    public string GenerateToken(string userId)
    {
        var key = Encoding.UTF8.GetBytes(_key);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
        };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}