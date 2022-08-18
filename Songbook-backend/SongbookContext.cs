
using Songbook_backend.Logger.Models;
using Songbook_backend.Songs.Models;

namespace Songbook_backend;

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
    public DbSet<Edition> Editions { get; set; }

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

            if (!SongTypes.Any())
            {
                var types = GetSongTypes();
                SongTypes.AddRange(types);
                SaveChanges();
            }

            if (!SongGroups.Any())
            {
                var groups = GetSongGroups();
                SongGroups.AddRange(groups);
                SaveChanges();
            }
        }
    }

    private IEnumerable<UserRole> GetRoles()
    {
        var roles = new List<UserRole>()
        {
            new UserRole() { Name = "User" },

            new UserRole() { Name = "Editor" },

            new UserRole() { Name = "Admin" }
        };

        return roles;
    }

    private IEnumerable<SongGroup> GetSongGroups()
    {
        var groups = new List<SongGroup>()
        {
            new SongGroup() { Name = "Group1" },
            new SongGroup() { Name = "Group2" },
            new SongGroup() { Name = "Group3" }
        };

        return groups;
    }
    
    private IEnumerable<SongType> GetSongTypes()
    {
        var types = new List<SongType>()
        {
            new SongType() { Name = "Type1" },
            new SongType() { Name = "Type2" },
            new SongType() { Name = "Type3" }
        };

        return types;
    }

    private IEnumerable<SongPart> GetSongParts()
    {
        var songParts = new List<SongPart>()
        {
            new SongPart() { Name = "Intro" },
            new SongPart() { Name = "Stanza" },
            new SongPart() { Name = "Refrain" },
            new SongPart() { Name = "Bridge" },
        };

        return songParts;
    }
}
