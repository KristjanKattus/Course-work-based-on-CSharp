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
    public class GameTeamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameTeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GameTeam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game_Team>>> GetGameTeams()
        {
            return await _context.GameTeams.ToListAsync();
        }

        // GET: api/GameTeam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game_Team>> GetGame_Team(Guid id)
        {
            var game_Team = await _context.GameTeams.FindAsync(id);

            if (game_Team == null)
            {
                return NotFound();
            }

            return game_Team;
        }

        // PUT: api/GameTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame_Team(Guid id, Game_Team game_Team)
        {
            if (id != game_Team.Id)
            {
                return BadRequest();
            }

            _context.Entry(game_Team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_TeamExists(id))
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

        // POST: api/GameTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game_Team>> PostGame_Team(Game_Team game_Team)
        {
            _context.GameTeams.Add(game_Team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame_Team", new { id = game_Team.Id }, game_Team);
        }

        // DELETE: api/GameTeam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame_Team(Guid id)
        {
            var game_Team = await _context.GameTeams.FindAsync(id);
            if (game_Team == null)
            {
                return NotFound();
            }

            _context.GameTeams.Remove(game_Team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Game_TeamExists(Guid id)
        {
            return _context.GameTeams.Any(e => e.Id == id);
        }
    }
}
