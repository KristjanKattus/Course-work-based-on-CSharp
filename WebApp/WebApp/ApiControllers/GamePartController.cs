using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamePartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GamePart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game_Part>>> GetGameParts()
        {
            return await _context.GameParts.ToListAsync();
        }

        // GET: api/GamePart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game_Part>> GetGame_Part(Guid id)
        {
            var game_Part = await _context.GameParts.FindAsync(id);

            if (game_Part == null)
            {
                return NotFound();
            }

            return game_Part;
        }

        // PUT: api/GamePart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame_Part(Guid id, Game_Part game_Part)
        {
            if (id != game_Part.Id)
            {
                return BadRequest();
            }

            _context.Entry(game_Part).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_PartExists(id))
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

        // POST: api/GamePart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game_Part>> PostGame_Part(Game_Part game_Part)
        {
            _context.GameParts.Add(game_Part);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame_Part", new { id = game_Part.Id }, game_Part);
        }

        // DELETE: api/GamePart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame_Part(Guid id)
        {
            var game_Part = await _context.GameParts.FindAsync(id);
            if (game_Part == null)
            {
                return NotFound();
            }

            _context.GameParts.Remove(game_Part);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Game_PartExists(Guid id)
        {
            return _context.GameParts.Any(e => e.Id == id);
        }
    }
}
