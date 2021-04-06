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
    public class ClubTeamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClubTeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ClubTeam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Club_Team>>> GetClubTeams()
        {
            return await _context.ClubTeams.ToListAsync();
        }

        // GET: api/ClubTeam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Club_Team>> GetClub_Team(Guid id)
        {
            var club_Team = await _context.ClubTeams.FindAsync(id);

            if (club_Team == null)
            {
                return NotFound();
            }

            return club_Team;
        }

        // PUT: api/ClubTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub_Team(Guid id, Club_Team club_Team)
        {
            if (id != club_Team.Id)
            {
                return BadRequest();
            }

            _context.Entry(club_Team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Club_TeamExists(id))
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

        // POST: api/ClubTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Club_Team>> PostClub_Team(Club_Team club_Team)
        {
            _context.ClubTeams.Add(club_Team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClub_Team", new { id = club_Team.Id }, club_Team);
        }

        // DELETE: api/ClubTeam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub_Team(Guid id)
        {
            var club_Team = await _context.ClubTeams.FindAsync(id);
            if (club_Team == null)
            {
                return NotFound();
            }

            _context.ClubTeams.Remove(club_Team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Club_TeamExists(Guid id)
        {
            return _context.ClubTeams.Any(e => e.Id == id);
        }
    }
}
