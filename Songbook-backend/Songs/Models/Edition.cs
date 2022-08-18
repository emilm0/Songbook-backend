namespace Songbook_backend.Songs.Models;

public class Edition
{
    public Guid Id { get; set; }
    public Guid SongId { get; set; }
    public string EditorName { get; set; }
    public string Comment { get; set; }
    public DateTime CreateDate { get; set; }
}
