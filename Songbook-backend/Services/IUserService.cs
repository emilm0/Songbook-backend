using TestSongbook.Models;
using TestSongbook.Models.Requests;

namespace TestSongbook.Services;

public interface IUserService
{
    User CreateUser(RegisterRequest request, byte[] passwordHash, byte[] passwordSalt);
    bool FindUserInDatabaseByEmail(string email);
    string GetUserRoleNameById(Guid id);
}