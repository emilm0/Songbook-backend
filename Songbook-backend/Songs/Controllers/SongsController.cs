using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Songbook_backend.Songs.Models;
using Songbook_backend.Songs.Models.Request;
using Songbook_backend.Songs.Services;
using System.Security.Claims;

namespace Songbook_backend.Songs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SongbookContext _context;
        private readonly ISongService _songService;
        private readonly IEditionService _editionService;
        private readonly ILineService _lineService;

        public SongsController(SongbookContext context, 
                                ISongService songService, 
                                IEditionService editionService, 
                                ILineService lineService)
        {
            _context = context;
            _songService = songService;
            _editionService = editionService;
            _lineService = lineService;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            return await _context.Songs.ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(Guid id)
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(Guid id, EditSongRequest songRequest)
        {

            if(!SongExists(id))
            {
                return NotFound();
            }

            if (_songService.TitleIsAlreadyUsed(songRequest.Title))
            {
                return BadRequest("Title is already taken");

            }

            if (_songService.TitleOriginIsAlreadyUsed(songRequest.TitleOrigin))
            {
                return BadRequest("TitleOrigin is already taken");

            }
            //var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var userName = "TestE";

            var song = _songService.UpdateSong(id, songRequest, userName);
            
            _context.Entry(song).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong([FromBody] CreateSongRequest songRequest)
        {
            if(_songService.TitleIsAlreadyUsed(songRequest.Title))
            {
                return BadRequest("Title is already taken");

            }

            if(_songService.TitleOriginIsAlreadyUsed(songRequest.TitleOrigin))
            {
                return BadRequest("TitleOrigin is already taken");

            }
            //var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var userName = "Test";

            var newSong = _songService.CreateSong(songRequest, userName);
            _context.Songs.Add(newSong);

            var lines = _lineService.CreateLineList(newSong.Id, songRequest.Lines);
            _context.Lines.AddRange(lines);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _lineService.DeleteLines(song.Id);
            _editionService.DeleteEdition(song.Id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(Guid id)
        {
            return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }

}
