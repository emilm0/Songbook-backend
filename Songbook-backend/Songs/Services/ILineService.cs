using Songbook_backend.Songs.Models.Request;
using Songbook_backend.Songs.Models;

namespace Songbook_backend.Songs.Services;

public interface ILineService
{
    public Line CreateLine(Guid songId, LineRequest lineRequest);
    public IEnumerable<Line> CreateLineList(Guid songId, List<LineRequest> linesRequest);


}