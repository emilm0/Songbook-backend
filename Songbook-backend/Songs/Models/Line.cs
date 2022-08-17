namespace Songbook_backend.Songs.Models;

public class Line
{
    public Guid Id { get; set; }
    public Guid SongId { get; set; }
    public string TextPL { get; set; }
    public string TextOrigin { get; set; }
    public string Chords { get; set; }
    public string ChordsOrigin { get; set; }
    public Guid SongPartId { get; set; }
    public int SongPartNumber { get; set; }
    public int LinePosition { get; set; }
}
