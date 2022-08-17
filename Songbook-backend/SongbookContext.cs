using Songbook_backend.Logger.Models;
using Songbook_backend.Songs.Models;

namespace TestSongbook;

public class SongbookContext : DbContext
{

    public SongbookContext(DbContextOptions<SongbookContext> options)
        : base(options)
    {
        Seed();
    }

    //Logger
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    //Song
    public DbSet<Song> Songs { get; set; }
    public DbSet<Line> Lines { get; set; }
    public DbSet<SongGroup> SongGroups { get; set; }
    public DbSet<SongType> SongTypes { get; set; }
    public DbSet<SongPart> SongParts { get; set; }

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
