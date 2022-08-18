using Songbook_backend.Songs.Models;

namespace Songbook_backend.Songs.Services
{
    public interface IEditionService
    {
        public Edition CreateEdition(Guid songId, string editionComment, string editorName);
        public void DeleteEdition(Guid songId);
    }
}