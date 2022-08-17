using Songbook_backend.Songs.Models;
using Songbook_backend.Songs.Models.Request;

namespace Songbook_backend.Songs.Services;

public interface ISongService
{
    public Song CreateSong(CreateSongRequest song);
    public bool TitleIsAlreadyUsed (string title);
}