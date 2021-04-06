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
    public class StadiumAreaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StadiumAreaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StadiumArea
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stadium_Area>>> GetStadiumAreas()
        {
            return await _context.StadiumAreas.ToListAsync();
        }

        // GET: api/StadiumArea/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stadium_Area>> GetStadium_Area(Guid id)
        {
            var stadium_Area = await _context.StadiumAreas.FindAsync(id);

            if (stadium_Area == null)
            {
                return NotFound();
            }

            return stadium_Area;
        }

        // PUT: api/StadiumArea/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStadium_Area(Guid id, Stadium_Area stadium_Area)
        {
            if (id != stadium_Area.Id)
            {
                return BadRequest();
            }

            _context.Entry(stadium_Area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stadium_AreaExists(id))
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

        // POST: api/StadiumArea
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stadium_Area>> PostStadium_Area(Stadium_Area stadium_Area)
        {
            _context.StadiumAreas.Add(stadium_Area);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStadium_Area", new { id = stadium_Area.Id }, stadium_Area);
        }

        // DELETE: api/StadiumArea/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStadium_Area(Guid id)
        {
            var stadium_Area = await _context.StadiumAreas.FindAsync(id);
            if (stadium_Area == null)
            {
                return NotFound();
            }

            _context.StadiumAreas.Remove(stadium_Area);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Stadium_AreaExists(Guid id)
        {
            return _context.StadiumAreas.Any(e => e.Id == id);
        }
    }
}
