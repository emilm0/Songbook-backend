using Songbook_backend.Logger.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using TestSongbook.Models.Responses;
using TestSongbook.Services.Tokens;

namespace Songbook_backend.Logger.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly SongbookContext _context;


    public AuthService(IConfiguration configuration,
        IUserService userService,
        ITokenGenerator tokenGenerator,
        SongbookContext context)
    {
        _configuration = configuration;
        _userService = userService;
        _tokenGenerator = tokenGenerator;
        _context = context;
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    public string CreateAccessToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Role, _userService.GetUserRoleNameById(user.RoleId))
        };

        var token = _tokenGenerator.GenereteToken(
            _configuration["Authentication:AccessTokenSecret"],
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            int.Parse(_configuration["Authentication:AccessTokenExpirationMinutes"]),
            claims
            );

        return token;
    }

    public string CreateRefreshToken()
    {
        var token = _tokenGenerator.GenereteToken(
            _configuration["Authentication:RefreshTokenSecret"],
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            int.Parse(_configuration["Authentication:RefreshTokenExpirationMinutes"]));

        return token;
    }

    public AuthenticatedUserResponse Authenticate(User user)
    {
        string accessToken = CreateAccessToken(user);
        string refreshToken = CreateRefreshToken();

        var refreshTokenDto = new RefreshToken()
        {
            Token = refreshToken,
            UserId = user.Id,
        };

        _context.RefreshTokens.Add(refreshTokenDto);
        _context.SaveChangesAsync();

        return new AuthenticatedUserResponse()
        {
            Username = user.FirstName,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public void DeleteAllRefreshTokensByUserId(Guid userId)
    {
        var refreshToken = _context.RefreshTokens.Find(userId);

        while (refreshToken != null)
        {
            _context.Remove(refreshToken);
            _context.SaveChanges();
            refreshToken = _context.RefreshTokens.Find(userId);
        }

        _context.SaveChanges();
    }
}
