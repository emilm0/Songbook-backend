using System.ComponentModel.DataAnnotations;

namespace Songbook_backend.Songs.Models.Request;

public class EditSongRequest
{
    [MaxLength(50)]
    public string Title { get; set; }
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
    public bool IsRedayToUse { get; set; }
    [Required]
    public string EditionComment { get; set; }
    public List<EditLineRequest> Lines { get; set; }
}
