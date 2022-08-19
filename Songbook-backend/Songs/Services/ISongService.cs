using Songbook_backend.Songs.Models;
using Songbook_backend.Songs.Models.Request;

namespace Songbook_backend.Songs.Services;

public interface ISongService
{
    public Song CreateSong(CreateSongRequest song, string creatorName);
    public Guid SongIdWithTheSameTitles(string title, string titleOrigin);
    public Guid FindSongIdByTitle(string title);
    public Song UpdateSong(Guid id, EditSongRequest songRequest, string editorName);
}