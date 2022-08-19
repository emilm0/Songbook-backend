namespace Songbook_backend.Songs.Models.Request;

public class EditLineRequest
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public string? TextOrigin { get; set; }
    public string? Chords { get; set; }
    public string? ChordsOrigin { get; set; }

}
