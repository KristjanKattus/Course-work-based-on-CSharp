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
    public class TeamPersonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamPersonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TeamPerson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team_Person>>> GetTeamPersons()
        {
            return await _context.TeamPersons.ToListAsync();
        }

        // GET: api/TeamPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team_Person>> GetTeam_Person(Guid id)
        {
            var team_Person = await _context.TeamPersons.FindAsync(id);

            if (team_Person == null)
            {
                return NotFound();
            }

            return team_Person;
        }

        // PUT: api/TeamPerson/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam_Person(Guid id, Team_Person team_Person)
        {
            if (id != team_Person.Id)
            {
                return BadRequest();
            }

            _context.Entry(team_Person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Team_PersonExists(id))
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

        // POST: api/TeamPerson
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team_Person>> PostTeam_Person(Team_Person team_Person)
        {
            _context.TeamPersons.Add(team_Person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam_Person", new { id = team_Person.Id }, team_Person);
        }

        // DELETE: api/TeamPerson/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam_Person(Guid id)
        {
            var team_Person = await _context.TeamPersons.FindAsync(id);
            if (team_Person == null)
            {
                return NotFound();
            }

            _context.TeamPersons.Remove(team_Person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Team_PersonExists(Guid id)
        {
            return _context.TeamPersons.Any(e => e.Id == id);
        }
    }
}
