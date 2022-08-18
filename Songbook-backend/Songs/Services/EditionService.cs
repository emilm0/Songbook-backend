using Songbook_backend.Songs.Models;

namespace Songbook_backend.Songs.Services;

public class EditionService : IEditionService
{
    private readonly SongbookContext _context;
    public EditionService(SongbookContext context)
    {
        _context = context;
    }

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

    public void DeleteEdition(Guid songId)
    {
        var edition = _context.Editions.FirstOrDefault(e => e.SongId == songId);

        while(edition != null)
        {
            _context.Editions.Remove(edition);
            _context.SaveChanges();
            edition = _context.Editions.FirstOrDefault(e => e.SongId == songId);
        }
    }

}
