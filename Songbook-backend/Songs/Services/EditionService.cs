using Songbook_backend.Songs.Models;

namespace Songbook_backend.Songs.Services;

public class EditionService : IEditionService
{
    public Edition CreateEdition(Guid songId, string editionComment, string editorName)
    {
        var edition = new Edition()
        {
            SongId = songId,
            EditorName = editorName,
            Comment = editionComment,
            CreateDate = DateTime.Now,
        };

        return edition;
    }
}
