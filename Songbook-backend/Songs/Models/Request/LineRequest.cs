namespace Songbook_backend.Songs.Models.Request;

public class LineRequest
{
    public string Text { get; set; }
    public string? TextOrigin { get; set; }
    public string? Chords { get; set; }
    public string? ChordsOrigin { get; set; }
    public string SongPartName { get; set; }
    public int SongPartNumber { get; set; }
    public int LinePosition { get; set; }
}
