using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LeageTeamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeageTeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LeageTeam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<League_Team>>> GetLeagueTeams()
        {
            return await _context.LeagueTeams.ToListAsync();
        }

        // GET: api/LeageTeam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<League_Team>> GetLeague_Team(Guid id)
        {
            var league_Team = await _context.LeagueTeams.FindAsync(id);

            if (league_Team == null)
            {
                return NotFound();
            }

            return league_Team;
        }

        // PUT: api/LeageTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeague_Team(Guid id, League_Team league_Team)
        {
            if (id != league_Team.Id)
            {
                return BadRequest();
            }

            _context.Entry(league_Team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!League_TeamExists(id))
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

        // POST: api/LeageTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<League_Team>> PostLeague_Team(League_Team league_Team)
        {
            _context.LeagueTeams.Add(league_Team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeague_Team", new { id = league_Team.Id }, league_Team);
        }

        // DELETE: api/LeageTeam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague_Team(Guid id)
        {
            var league_Team = await _context.LeagueTeams.FindAsync(id);
            if (league_Team == null)
            {
                return NotFound();
            }

            _context.LeagueTeams.Remove(league_Team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool League_TeamExists(Guid id)
        {
            return _context.LeagueTeams.Any(e => e.Id == id);
        }
    }
}
