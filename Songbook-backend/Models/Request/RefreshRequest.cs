using System.ComponentModel.DataAnnotations;

namespace TestSongbook.Models.Requests;

public class RefreshRequest
{
    [Required]
    public string RefreshToken { get; set; }
}
