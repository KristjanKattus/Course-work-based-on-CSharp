using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class EventTypeController : Controller
    {
        private readonly AppDbContext _context;

        public EventTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EventType
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventTypes.ToListAsync());
        }

        // GET: EventType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var event_Type = await _context.EventTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (event_Type == null)
            {
                return NotFound();
            }

            return View(event_Type);
        }

        // GET: EventType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] Event_Type event_Type)
        {
            if (ModelState.IsValid)
            {
                event_Type.Id = Guid.NewGuid();
                _context.Add(event_Type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(event_Type);
        }

        // GET: EventType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var event_Type = await _context.EventTypes.FindAsync(id);
            if (event_Type == null)
            {
                return NotFound();
            }
            return View(event_Type);
        }

        // POST: EventType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] Event_Type event_Type)
        {
            if (id != event_Type.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(event_Type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Event_TypeExists(event_Type.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(event_Type);
        }

        // GET: EventType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var event_Type = await _context.EventTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (event_Type == null)
            {
                return NotFound();
            }

            return View(event_Type);
        }

        // POST: EventType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var event_Type = await _context.EventTypes.FindAsync(id);
            _context.EventTypes.Remove(event_Type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Event_TypeExists(Guid id)
        {
            return _context.EventTypes.Any(e => e.Id == id);
        }
    }
}
