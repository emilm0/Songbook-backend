namespace Songbook_backend.Songs.Models;

public class Song
{
    public Guid Id { get; set; }
    public string Signature { get; set; }
    public string TitlePl {  get; set; }
    public string TitleOrigin { get; set; }
    public string Key { get; set; }
    public string KeyOrigin { get; set; }
    public int Tempo { get; set; }
    public string Author { get; set; }
    public string Translator { get; set; }
    public string Copyright { get; set; }
    public string BasedOn { get; set; }
    public Guid GroupId { get; set; }
    public Guid TypeId { get; set; }
    public DateTime CreateTime { get; set; }
    public string UrlPl { get; set; }
    public string UrlOrigin { get; set; }
    public string UrlDrive { get; set; }
    public string UrlNotes { get; set; }
    public Guid? EditId { get; set; }
    public bool IsReadyToUser { get; set; }
    public bool IsInUse { get; set; }
    public DateTime? LastUsed { get; set; }
    public int CounterOfUse { get; set; }
}
