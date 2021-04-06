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
    public class EventTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EventType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event_Type>>> GetEventTypes()
        {
            return await _context.EventTypes.ToListAsync();
        }

        // GET: api/EventType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event_Type>> GetEvent_Type(Guid id)
        {
            var event_Type = await _context.EventTypes.FindAsync(id);

            if (event_Type == null)
            {
                return NotFound();
            }

            return event_Type;
        }

        // PUT: api/EventType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent_Type(Guid id, Event_Type event_Type)
        {
            if (id != event_Type.Id)
            {
                return BadRequest();
            }

            _context.Entry(event_Type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Event_TypeExists(id))
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

        // POST: api/EventType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event_Type>> PostEvent_Type(Event_Type event_Type)
        {
            _context.EventTypes.Add(event_Type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent_Type", new { id = event_Type.Id }, event_Type);
        }

        // DELETE: api/EventType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent_Type(Guid id)
        {
            var event_Type = await _context.EventTypes.FindAsync(id);
            if (event_Type == null)
            {
                return NotFound();
            }

            _context.EventTypes.Remove(event_Type);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Event_TypeExists(Guid id)
        {
            return _context.EventTypes.Any(e => e.Id == id);
        }
    }
}
