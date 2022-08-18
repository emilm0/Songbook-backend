﻿using Microsoft.AspNetCore.Authorization;
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

        public SongsController(SongbookContext context, ISongService songService)
        {
            _context = context;
            _songService = songService;
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
            //var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var userName = "TestE";

            var song = _songService.UpdateSong(id, songRequest, userName);
            
            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong([FromBody] CreateSongRequest song)
        {
            if (_songService.TitleIsAlreadyUsed(song.Title))
            {
                return BadRequest("Title is already taken");

            }
            //var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var userName = "Test";

            var newSong = _songService.CreateSong(song, userName);

            _context.Songs.Add(newSong);
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
