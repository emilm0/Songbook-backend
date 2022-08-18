using Songbook_backend.Logger.Models;
using TestSongbook.Models.Requests;

namespace Songbook_backend.Logger.Services;

public class UserService : IUserService
{
    private readonly SongbookContext _context;

    public UserService(SongbookContext context)
    {
        _context = context;
    }

    public User CreateUser(RegisterRequest request, byte[] passwordHash, byte[] passwordSalt)
    {
        var user = new User()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RoleId = SetUserRoleId(request.UserRole)
        };

        return user;
    }

    public string GetUserRoleNameById(Guid id)
    {
        return _context.UserRoles.Find(id).Name;

    }

    private Guid SetUserRoleId(string userRoleName)
    {
        var userRole = _context.UserRoles.FirstOrDefault(r => r.Name == userRoleName);

        if (userRole == null)
        {
            userRole = _context.UserRoles.FirstOrDefault(r => r.Name == "User");
            return userRole.Id;
        }

        return userRole.Id;
    }


    public bool FindUserInDatabaseByEmail(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        return user != null ? true : false;

    }
}
