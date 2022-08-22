using Songbook_backend.Songs.Models;
using Songbook_backend.Songs.Models.Request;
using Songbook_backend.Songs.Models.Response;
using System.Data;

namespace Songbook_backend.Songs.Services;

public class SongService : ISongService
{
    private readonly SongbookContext _context;
    private readonly IEditionService _editionService;
    private readonly ILineService _lineService;

    public SongService(SongbookContext context, IEditionService editionService, ILineService lineService)
    {
        _context = context;
        _editionService = editionService;
        _lineService = lineService;
    }

    public IEnumerable<SongResponse> GetSongsResponse()
    {
        var songs =  _context.Songs.ToList();
        var songsResponse = new List<SongResponse>();
        foreach(var song in songs)
        {
            songsResponse.Add(GetSongResponse(song.Id));
        }

        return songsResponse.AsEnumerable();
    }
    public SongResponse GetSongResponse(Guid id)
    {
        var song = _context.Songs.Find(id);
        var lines = _lineService.GetLineList(id);

        SongResponse response = new SongResponse(song, lines);
        return response;
    }

    public Song CreateSong(CreateSongRequest songRequest, string creatorName)
    {
        var newSong = new Song()
        {
            Signature = CreateSongSignature(songRequest.Title),
            Title = songRequest.Title,
            TitleOrigin = songRequest.TitleOrigin,
            Key = songRequest.Key,
            KeyOrigin = songRequest.KeyOrigin,
            Tempo = songRequest.Tempo,
            Author = songRequest.Author,
            Translator = songRequest.Translator,
            Copyright = songRequest.Copyright,
            BasedOn = songRequest.BasedOn,
            GroupId = FindSongGroupByName(songRequest.Group),
            TypeId = FindSongTypeByName(songRequest.Type),
            CreateTime = DateTime.Now,
            UrlPl = songRequest.UrlPl,
            UrlOrigin = songRequest.UrlOrigin,
            UrlDrive = songRequest.UrlDrive,
            UrlNotes = songRequest.UrlNotes,
            EditId = Guid.Empty,
            IsReadyToUse = songRequest.IsRedayToUse,
            IsInUse = false,
            LastUsed = null,
            CounterOfUse = 0,
            CreatorName = creatorName
        };

        return newSong;
    }

    public Song UpdateSong(Guid id, EditSongRequest songRequest, string editorName)
    {
        var edition = _editionService.CreateEdition(id, songRequest.EditionComment, editorName);
        _context.Editions.Add(edition);

        var updatedSong = _context.Songs.Find(id);
        {
            if(updatedSong.Title != songRequest.Title)
            {
                updatedSong.Signature = CreateSongSignature(songRequest.Title);
            }
            updatedSong.Title = songRequest.Title;
            updatedSong.TitleOrigin = songRequest.TitleOrigin;
            updatedSong.Key = songRequest.Key;
            updatedSong.KeyOrigin = songRequest.KeyOrigin;
            updatedSong.Tempo = songRequest.Tempo;
            updatedSong.Author = songRequest.Author;
            updatedSong.Translator = songRequest.Translator;
            updatedSong.Copyright = songRequest.Copyright;
            updatedSong.BasedOn = songRequest.BasedOn;
            updatedSong.GroupId = FindSongGroupByName(songRequest.Group);
            updatedSong.TypeId = FindSongTypeByName(songRequest.Type);
            updatedSong.UrlPl = songRequest.UrlPl;
            updatedSong.UrlOrigin = songRequest.UrlOrigin;
            updatedSong.UrlDrive = songRequest.UrlDrive;
            updatedSong.UrlNotes = songRequest.UrlNotes;
            updatedSong.EditId = edition.Id;
            updatedSong.IsReadyToUse = songRequest.IsRedayToUse;
        };

        return updatedSong;
    }

    public Guid SongIdWithTheSameTitles(string title, string titleOrigin)
    {
        var song = _context.Songs.FirstOrDefault(s => s.Title == title);
        if(song == null)
        {
            song = _context.Songs.FirstOrDefault(s => s.TitleOrigin == titleOrigin);
            if( song == null)
            {
                return Guid.Empty;
            }

            return song.Id;
        }
        return song.Id;
    }

    public Guid FindSongIdByTitle(string title)
    {
        var song = _context.Songs.FirstOrDefault(s => s.Title == title);
        return song != null ? song.Id : Guid.Empty;
    }

    private Guid FindSongTypeByName(string type)
    {
        var songType = _context.SongTypes.FirstOrDefault(t => t.Name == type);
        return songType != null ? songType.Id : Guid.Empty;
    }

    private Guid FindSongGroupByName(string group)
    {
        var songGroup = _context.SongGroups.FirstOrDefault(g => g.Name == group);
        return songGroup != null ? songGroup.Id : Guid.Empty ;
    }

    private string CreateSongSignature(string songTitle)
    {
        string firstLetter = songTitle.Substring(0, 1).ToUpper();

        var signature = FindEmptySignature(firstLetter);
        return signature;

    }

    private string FindEmptySignature(string firstLetter)
    {
        var songs = from s in _context.Songs
                    where s.Title.StartsWith(firstLetter)
                    select s;

        int counterOfSongs = songs.Count();

        for(int i = 1; i <= counterOfSongs; i++)
        {
            var song = _context.Songs.FirstOrDefault(s => s.Signature == firstLetter + i);
            if (song == null)
            {
                return firstLetter + i;
            }
        }

        return firstLetter + (counterOfSongs + 1).ToString();
    }


}

