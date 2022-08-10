using Microsoft.EntityFrameworkCore;
using Songbook_backend.Logger.Models;

namespace TestSongbook;

public class SongbookContext : DbContext
{

    public SongbookContext(DbContextOptions<SongbookContext> options)
        : base(options)
    {
        Seed();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    public void Seed()
    {
        if (Database.CanConnect())
        {
            if (!UserRoles.Any())
            {
                var roles = GetRoles();
                UserRoles.AddRange(roles);
                SaveChanges();
            }
        }
    }

    private IEnumerable<UserRole> GetRoles()
    {
        var roles = new List<UserRole>()
        {
            new UserRole()
            {
                Name = "User"
            },

            new UserRole()
            {
                Name = "Editor"
            },

            new UserRole()
            {
                Name = "Admin"
            }
        };

        return roles;
    }

}
