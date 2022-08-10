using System.ComponentModel.DataAnnotations;

namespace TestSongbook.Models.Requests;

public class RegisterRequest
{
    [EmailAddress]
    public string Email { get; set; }
    [StringLength(25, MinimumLength = 8, ErrorMessage = "Password length should be between 8 and 25 character")]
    public string Password { get; set; }
    [StringLength(30, ErrorMessage = "First Name length should be have max 30 characters ")]
    public string FirstName { get; set; }
    [StringLength(30, ErrorMessage = "First Name length should be have max 30 characters ")]
    public string LastName { get; set; }
    public string UserRole { get; set; }
}
