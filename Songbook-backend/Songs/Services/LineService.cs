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

    public List<Line> GetLineList(Guid songId)
    {
        var lines = _context.Lines.Where(l => l.SongId == songId).ToList();
        return lines;        
        
    }
    public List<Line> CreateLineList(Guid songId, List<LineRequest> linesRequest)
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

    public Line? UpdateLine(EditLineRequest lineRequest)
    {
        if(lineRequest.Text == "")
        {
            DeleteLine(lineRequest.Id);
            return null;
        }

        var updatedLine = _context.Lines.Find(lineRequest.Id);

        {
            updatedLine.Text = lineRequest.Text;
            updatedLine.TextOrigin = lineRequest.TextOrigin;
            updatedLine.Chords = lineRequest.Chords;
            updatedLine.ChordsOrigin = lineRequest.ChordsOrigin;
        }
        return updatedLine;
    }
    public void DeleteLine(Guid lineId)
    {
        var line = _context.Lines.Find(lineId);
        _context.Lines.Remove(line);
        _context.SaveChanges();
    }
    public void DeleteLines(Guid songId)
    {
        var line = _context.Lines.FirstOrDefault(l => l.SongId == songId);
        while(line != null)
        {
            _context.Lines.Remove(line);
            _context.SaveChanges();
            line = _context.Lines.FirstOrDefault(l => l.SongId == songId);
        }
    }

    private Guid FindSongPartIdByName(string songPartName)
    {
        var songPart = _context.SongParts.FirstOrDefault(p => p.Name == songPartName);
        return songPart != null ? songPart.Id : Guid.Empty;
    }
}
