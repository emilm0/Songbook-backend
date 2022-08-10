using Songbook_backend.Logger.Models;
using TestSongbook.Models.Requests;

namespace Songbook_backend.Logger.Services;

public interface IUserService
{
    User CreateUser(RegisterRequest request, byte[] passwordHash, byte[] passwordSalt);
    bool FindUserInDatabaseByEmail(string email);
    string GetUserRoleNameById(Guid id);
}