using System.Security.Claims;

namespace TestSongbook.Services.Tokens
{
    public interface ITokenGenerator
    {
        public string GenereteToken(string secretKey, string issuer, string audience, double expirationMinutes,
                                IEnumerable<Claim>? claims = null);
    }
}
