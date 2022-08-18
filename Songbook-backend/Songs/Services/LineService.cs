using Songbook_backend.Songs.Models;
using Songbook_backend.Songs.Models.Request;

namespace Songbook_backend.Songs.Services;

public class LineService : ILineService
{
    private readonly SongbookContext _context;

    public LineService(SongbookContext context)
    {
        _context = context;
    }
    public IEnumerable<Line> CreateLineList(Guid songId, List<LineRequest> linesRequest)
    {
        var lines = new List<Line>();
        foreach(var line in linesRequest)
        {
            lines.Add(CreateLine(songId, line));
        }

        return lines;
    }
    public Line CreateLine(Guid songId, LineRequest lineRequest)
    {
        var line = new Line()
        {
            SongId = songId,
            Text = lineRequest.Text,
            TextOrigin = lineRequest.TextOrigin,
            Chords = lineRequest.Chords,
            ChordsOrigin = lineRequest.ChordsOrigin,
            SongPartId = FindSongPartIdByName(lineRequest.SongPartName),
            SongPartNumber = lineRequest.SongPartNumber,
            LinePosition = lineRequest.LinePosition,
        };

        return line;
    }

    private Guid FindSongPartIdByName(string songPartName)
    {
        var songPart = _context.SongParts.FirstOrDefault(p => p.Name == songPartName);
        return songPart != null ? songPart.Id : Guid.Empty;
    }
}
