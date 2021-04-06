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
    public class GameTeamListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameTeamListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GameTeamList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game_Team_List>>> GetGameTeamListMembers()
        {
            return await _context.GameTeamListMembers.ToListAsync();
        }

        // GET: api/GameTeamList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game_Team_List>> GetGame_Team_List(Guid id)
        {
            var game_Team_List = await _context.GameTeamListMembers.FindAsync(id);

            if (game_Team_List == null)
            {
                return NotFound();
            }

            return game_Team_List;
        }

        // PUT: api/GameTeamList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame_Team_List(Guid id, Game_Team_List game_Team_List)
        {
            if (id != game_Team_List.Id)
            {
                return BadRequest();
            }

            _context.Entry(game_Team_List).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_Team_ListExists(id))
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

        // POST: api/GameTeamList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game_Team_List>> PostGame_Team_List(Game_Team_List game_Team_List)
        {
            _context.GameTeamListMembers.Add(game_Team_List);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame_Team_List", new { id = game_Team_List.Id }, game_Team_List);
        }

        // DELETE: api/GameTeamList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame_Team_List(Guid id)
        {
            var game_Team_List = await _context.GameTeamListMembers.FindAsync(id);
            if (game_Team_List == null)
            {
                return NotFound();
            }

            _context.GameTeamListMembers.Remove(game_Team_List);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Game_Team_ListExists(Guid id)
        {
            return _context.GameTeamListMembers.Any(e => e.Id == id);
        }
    }
}
