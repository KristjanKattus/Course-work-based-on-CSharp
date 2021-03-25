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
    public class GamePartTypeController : Controller
    {
        private readonly AppDbContext _context;

        public GamePartTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GamePartType
        public async Task<IActionResult> Index()
        {
            return View(await _context.Types.ToListAsync());
        }

        // GET: GamePartType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Part_Type = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Part_Type == null)
            {
                return NotFound();
            }

            return View(game_Part_Type);
        }

        // GET: GamePartType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GamePartType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] Game_Part_Type game_Part_Type)
        {
            if (ModelState.IsValid)
            {
                game_Part_Type.Id = Guid.NewGuid();
                _context.Add(game_Part_Type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game_Part_Type);
        }

        // GET: GamePartType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Part_Type = await _context.Types.FindAsync(id);
            if (game_Part_Type == null)
            {
                return NotFound();
            }
            return View(game_Part_Type);
        }

        // POST: GamePartType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] Game_Part_Type game_Part_Type)
        {
            if (id != game_Part_Type.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game_Part_Type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Game_Part_TypeExists(game_Part_Type.Id))
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
            return View(game_Part_Type);
        }

        // GET: GamePartType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Part_Type = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Part_Type == null)
            {
                return NotFound();
            }

            return View(game_Part_Type);
        }

        // POST: GamePartType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game_Part_Type = await _context.Types.FindAsync(id);
            _context.Types.Remove(game_Part_Type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Game_Part_TypeExists(Guid id)
        {
            return _context.Types.Any(e => e.Id == id);
        }
    }
}
