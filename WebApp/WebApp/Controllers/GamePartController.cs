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
    public class GamePartController : Controller
    {
        private readonly AppDbContext _context;

        public GamePartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GamePart
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GameParts.Include(g => g.Game).Include(g => g.GamePartType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GamePart/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Part = await _context.GameParts
                .Include(g => g.Game)
                .Include(g => g.GamePartType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Part == null)
            {
                return NotFound();
            }

            return View(game_Part);
        }

        // GET: GamePart/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name");
            return View();
        }

        // POST: GamePart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,GameId,NumberInOrder,Id")] Game_Part game_Part)
        {
            if (ModelState.IsValid)
            {
                game_Part.Id = Guid.NewGuid();
                _context.Add(game_Part);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Part.GameId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", game_Part.TypeId);
            return View(game_Part);
        }

        // GET: GamePart/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Part = await _context.GameParts.FindAsync(id);
            if (game_Part == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Part.GameId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", game_Part.TypeId);
            return View(game_Part);
        }

        // POST: GamePart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TypeId,GameId,NumberInOrder,Id")] Game_Part game_Part)
        {
            if (id != game_Part.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game_Part);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Game_PartExists(game_Part.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Part.GameId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", game_Part.TypeId);
            return View(game_Part);
        }

        // GET: GamePart/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Part = await _context.GameParts
                .Include(g => g.Game)
                .Include(g => g.GamePartType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Part == null)
            {
                return NotFound();
            }

            return View(game_Part);
        }

        // POST: GamePart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game_Part = await _context.GameParts.FindAsync(id);
            _context.GameParts.Remove(game_Part);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Game_PartExists(Guid id)
        {
            return _context.GameParts.Any(e => e.Id == id);
        }
    }
}
