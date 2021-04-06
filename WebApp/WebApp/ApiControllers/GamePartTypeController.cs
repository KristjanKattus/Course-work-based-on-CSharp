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
    public class GamePartTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamePartTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GamePartType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game_Part_Type>>> GetTypes()
        {
            return await _context.Types.ToListAsync();
        }

        // GET: api/GamePartType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game_Part_Type>> GetGame_Part_Type(Guid id)
        {
            var game_Part_Type = await _context.Types.FindAsync(id);

            if (game_Part_Type == null)
            {
                return NotFound();
            }

            return game_Part_Type;
        }

        // PUT: api/GamePartType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame_Part_Type(Guid id, Game_Part_Type game_Part_Type)
        {
            if (id != game_Part_Type.Id)
            {
                return BadRequest();
            }

            _context.Entry(game_Part_Type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Game_Part_TypeExists(id))
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

        // POST: api/GamePartType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game_Part_Type>> PostGame_Part_Type(Game_Part_Type game_Part_Type)
        {
            _context.Types.Add(game_Part_Type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame_Part_Type", new { id = game_Part_Type.Id }, game_Part_Type);
        }

        // DELETE: api/GamePartType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame_Part_Type(Guid id)
        {
            var game_Part_Type = await _context.Types.FindAsync(id);
            if (game_Part_Type == null)
            {
                return NotFound();
            }

            _context.Types.Remove(game_Part_Type);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Game_Part_TypeExists(Guid id)
        {
            return _context.Types.Any(e => e.Id == id);
        }
    }
}
