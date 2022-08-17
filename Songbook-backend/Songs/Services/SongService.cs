using Songbook_backend.Songs.Models;
using Songbook_backend.Songs.Models.Request;
using System.Data;

namespace Songbook_backend.Songs.Services;

public class SongService : ISongService
{
    private readonly SongbookContext _context;

    public SongService(SongbookContext context)
    {
        _context = context;
    }

    public Song CreateSong(CreateSongRequest song)
    {
        var newSong = new Song()
        {
            Signature = CreateSongSignature(song.TitlePl),
            TitlePl = song.TitlePl,
            TitleOrigin = song.TitleOrigin,
            Key = song.Key,
            KeyOrigin = song.KeyOrigin,
            Tempo = song.Tempo,
            Author = song.Author,
            Translator = song.Translator,
            Copyright = song.Copyright,
            BasedOn = song.BasedOn,
            GroupId = FindGroupByName(song.Group),
            TypeId = FindTypeByName(song.Type),
            CreateTime = DateTime.Now,
            UrlPl = song.UrlPl,
            UrlOrigin = song.UrlOrigin,
            UrlDrive = song.UrlDrive,
            UrlNotes = song.UrlNotes,
            EditId = Guid.Empty,
            IsReadyToUser = true,
            IsInUse = false,
            LastUsed = null,
            CounterOfUse = 0
        };

        return newSong;
    }

    public bool TitleIsAlreadyUsed(string title)
    {
        var song = _context.Songs.FirstOrDefault(s => s.TitlePl == title);
        if (song == null)
        {
            return false;
        }

        return true;
    }

    private Guid FindTypeByName(string type)
    {
        var songType = _context.SongTypes.FirstOrDefault(t => t.Name == type);
        return songType != null ? songType.Id : Guid.Empty;
    }

    private Guid FindGroupByName(string group)
    {
        var songGroup = _context.SongGroups.FirstOrDefault(g => g.Name == group);
        return songGroup != null ? songGroup.Id : Guid.Empty ;
    }

    private string CreateSongSignature(string songTitle)
    {
        string firstLetter = songTitle.Substring(0, 1).ToUpper();

        var songs = from s in _context.Songs
                    where s.TitlePl.StartsWith(firstLetter)
                    select s;

        string signature = firstLetter + (songs.Count() + 1).ToString();
        return signature;

    }


}

