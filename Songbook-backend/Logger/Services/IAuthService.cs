using Songbook_backend.Logger.Models;
using TestSongbook.Models.Responses;

namespace Songbook_backend.Logger.Services;

public interface IAuthService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    string CreateAccessToken(User user);
    string CreateRefreshToken();
    AuthenticatedUserResponse Authenticate(User user);
    void DeleteAllRefreshTokensByUserId(Guid userId);
}