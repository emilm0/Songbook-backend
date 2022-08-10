using System.ComponentModel.DataAnnotations;

namespace Songbook_backend.Logger.Models;

public class User
{
    public Guid Id { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public Guid RoleId { get; set; }
}
