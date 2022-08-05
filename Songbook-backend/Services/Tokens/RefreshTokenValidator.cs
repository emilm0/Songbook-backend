using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TestSongbook.Services.Tokens;

public class RefreshTokenValidator : IRefreshTokenValidator
{
    private readonly IConfiguration _configuration;

    public RefreshTokenValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool Validate(string refreshToken)
    {
        TokenValidationParameters validationParametrs = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration["Authentication:RefreshTokenSecret"])),
            ValidIssuer = _configuration["Authentication:Issuer"],
            ValidAudience = _configuration["Authentication:Audience"],
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero

        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(refreshToken, validationParametrs, out SecurityToken validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}
