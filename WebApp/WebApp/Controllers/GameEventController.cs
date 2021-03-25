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
    public class GameEventController : Controller
    {
        private readonly AppDbContext _context;

        public GameEventController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GameEvent
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GameEvents.Include(g => g.EventType).Include(g => g.Game).Include(g => g.GamePart).Include(g => g.GamePersonnel);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GameEvent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Event = await _context.GameEvents
                .Include(g => g.EventType)
                .Include(g => g.Game)
                .Include(g => g.GamePart)
                .Include(g => g.GamePersonnel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Event == null)
            {
                return NotFound();
            }

            return View(game_Event);
        }

        // GET: GameEvent/Create
        public IActionResult Create()
        {
            ViewData["EventTypeId"] = new SelectList(_context.EventTypes, "Id", "Name");
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["GamePartId"] = new SelectList(_context.GameParts, "Id", "Id");
            ViewData["GamePersonnelId"] = new SelectList(_context.GamePersonnels, "Id", "Id");
            return View();
        }

        // POST: GameEvent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,GamePersonnelId,GamePartId,EventTypeId,GameTime,NumberInOrder,Description,Id")] Game_Event game_Event)
        {
            if (ModelState.IsValid)
            {
                game_Event.Id = Guid.NewGuid();
                _context.Add(game_Event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventTypeId"] = new SelectList(_context.EventTypes, "Id", "Name", game_Event.EventTypeId);
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Event.GameId);
            ViewData["GamePartId"] = new SelectList(_context.GameParts, "Id", "Id", game_Event.GamePartId);
            ViewData["GamePersonnelId"] = new SelectList(_context.GamePersonnels, "Id", "Id", game_Event.GamePersonnelId);
            return View(game_Event);
        }

        // GET: GameEvent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Event = await _context.GameEvents.FindAsync(id);
            if (game_Event == null)
            {
                return NotFound();
            }
            ViewData["EventTypeId"] = new SelectList(_context.EventTypes, "Id", "Name", game_Event.EventTypeId);
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Event.GameId);
            ViewData["GamePartId"] = new SelectList(_context.GameParts, "Id", "Id", game_Event.GamePartId);
            ViewData["GamePersonnelId"] = new SelectList(_context.GamePersonnels, "Id", "Id", game_Event.GamePersonnelId);
            return View(game_Event);
        }

        // POST: GameEvent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GameId,GamePersonnelId,GamePartId,EventTypeId,GameTime,NumberInOrder,Description,Id")] Game_Event game_Event)
        {
            if (id != game_Event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game_Event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Game_EventExists(game_Event.Id))
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
            ViewData["EventTypeId"] = new SelectList(_context.EventTypes, "Id", "Name", game_Event.EventTypeId);
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Event.GameId);
            ViewData["GamePartId"] = new SelectList(_context.GameParts, "Id", "Id", game_Event.GamePartId);
            ViewData["GamePersonnelId"] = new SelectList(_context.GamePersonnels, "Id", "Id", game_Event.GamePersonnelId);
            return View(game_Event);
        }

        // GET: GameEvent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Event = await _context.GameEvents
                .Include(g => g.EventType)
                .Include(g => g.Game)
                .Include(g => g.GamePart)
                .Include(g => g.GamePersonnel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Event == null)
            {
                return NotFound();
            }

            return View(game_Event);
        }

        // POST: GameEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game_Event = await _context.GameEvents.FindAsync(id);
            _context.GameEvents.Remove(game_Event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Game_EventExists(Guid id)
        {
            return _context.GameEvents.Any(e => e.Id == id);
        }
    }
}
