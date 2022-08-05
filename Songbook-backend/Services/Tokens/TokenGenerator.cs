using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TestSongbook.Services.Tokens;

public class TokenGenerator : ITokenGenerator
{

    public string GenereteToken(string secretKey, string issuer, string audience, double expirationMinutes,
                                IEnumerable<Claim>? claims = null)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(expirationMinutes),
            creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}