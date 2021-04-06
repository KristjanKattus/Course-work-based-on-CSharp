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
    public class GameEventController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameEventController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GameEvent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game_Event>>> GetGameEvents()
        {
            return await _context.GameEvents.ToListAsync();
        }

        // GET: api/GameEvent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game_Event>> GetGame_Event(Guid id)
        {
            var game_Event = await _context.GameEvents.FindAsync(id);

            if (game_Event == null)
            {
                return NotFound();
            }

            return game_Event;
        }

        // PUT: api/GameEvent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame_Event(Guid id, Game_Event game_Event)
        {
            if (id != game_Event.Id)
            {
                return BadRequest();
            }

            _context.Entry(game_Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_EventExists(id))
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

        // POST: api/GameEvent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game_Event>> PostGame_Event(Game_Event game_Event)
        {
            _context.GameEvents.Add(game_Event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame_Event", new { id = game_Event.Id }, game_Event);
        }

        // DELETE: api/GameEvent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame_Event(Guid id)
        {
            var game_Event = await _context.GameEvents.FindAsync(id);
            if (game_Event == null)
            {
                return NotFound();
            }

            _context.GameEvents.Remove(game_Event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Game_EventExists(Guid id)
        {
            return _context.GameEvents.Any(e => e.Id == id);
        }
    }
}
