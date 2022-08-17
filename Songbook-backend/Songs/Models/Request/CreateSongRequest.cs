using System.ComponentModel.DataAnnotations;

namespace Songbook_backend.Songs.Models.Request;

public class CreateSongRequest
{
    [Required]
    [MaxLength(50)]
    public string TitlePl { get; set; }
    [MaxLength(50)]
    public string TitleOrigin { get; set; }
    [MaxLength(5)]
    public string Key { get; set; }
    [MaxLength(5)]
    public string KeyOrigin { get; set; }
    public int Tempo { get; set; }
    [MaxLength(50)] 
    public string Author { get; set; }
    [MaxLength(50)] 
    public string Translator { get; set; }
    [MaxLength(200)]
    public string Copyright { get; set; }
    [MaxLength(100)]
    public string BasedOn { get; set; }
    public string Group { get; set; }
    public string Type { get; set; } 
    public string UrlPl { get; set; }
    public string UrlOrigin { get; set; }
    public string UrlDrive { get; set; }
    public string UrlNotes { get; set; }
}
