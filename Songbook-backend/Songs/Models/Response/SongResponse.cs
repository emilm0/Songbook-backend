namespace Songbook_backend.Songs.Models.Response;

public class SongResponse : Song
{
    public SongResponse(Song song, List<Line> lines)
    {
        Id = song.Id;
        Signature = song.Signature;
        Title = song.Title;
        TitleOrigin = song.TitleOrigin;
        Key = song.Key;
        KeyOrigin = song.KeyOrigin;
        Tempo = song.Tempo;
        Author = song.Author;
        Translator = song.Translator;
        Copyright = song.Copyright;
        BasedOn = song.BasedOn;
        GroupId = song.GroupId;
        TypeId = song.TypeId;
        CreateTime = song.CreateTime;
        CreatorName = song.CreatorName;
        UrlPl = song.UrlPl;
        UrlOrigin = song.UrlOrigin;
        UrlDrive = song.UrlDrive;
        UrlNotes = song.UrlNotes;
        EditId = song.EditId;
        IsReadyToUse = song.IsReadyToUse;
        IsInUse = song.IsInUse;
        LastUsed = song.LastUsed;
        CounterOfUse = song.CounterOfUse;
        Lines = lines;
    }
    public List<Line> Lines { get; set; }
}
