using System.ComponentModel.DataAnnotations;

namespace TestSongbook.Models.Requests;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
