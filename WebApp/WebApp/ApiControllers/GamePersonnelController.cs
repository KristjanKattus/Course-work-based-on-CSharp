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
    public class GamePersonnelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamePersonnelController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GamePersonnel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game_Personnel>>> GetGamePersonnels()
        {
            return await _context.GamePersonnels.ToListAsync();
        }

        // GET: api/GamePersonnel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game_Personnel>> GetGame_Personnel(Guid id)
        {
            var game_Personnel = await _context.GamePersonnels.FindAsync(id);

            if (game_Personnel == null)
            {
                return NotFound();
            }

            return game_Personnel;
        }

        // PUT: api/GamePersonnel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame_Personnel(Guid id, Game_Personnel game_Personnel)
        {
            if (id != game_Personnel.Id)
            {
                return BadRequest();
            }

            _context.Entry(game_Personnel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_PersonnelExists(id))
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

        // POST: api/GamePersonnel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game_Personnel>> PostGame_Personnel(Game_Personnel game_Personnel)
        {
            _context.GamePersonnels.Add(game_Personnel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame_Personnel", new { id = game_Personnel.Id }, game_Personnel);
        }

        // DELETE: api/GamePersonnel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame_Personnel(Guid id)
        {
            var game_Personnel = await _context.GamePersonnels.FindAsync(id);
            if (game_Personnel == null)
            {
                return NotFound();
            }

            _context.GamePersonnels.Remove(game_Personnel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Game_PersonnelExists(Guid id)
        {
            return _context.GamePersonnels.Any(e => e.Id == id);
        }
    }
}
