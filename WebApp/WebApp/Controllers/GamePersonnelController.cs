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
    public class GamePersonnelController : Controller
    {
        private readonly AppDbContext _context;

        public GamePersonnelController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GamePersonnel
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GamePersonnels.Include(g => g.Game).Include(g => g.Person).Include(g => g.Role);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GamePersonnel/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Personnel = await _context.GamePersonnels
                .Include(g => g.Game)
                .Include(g => g.Person)
                .Include(g => g.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Personnel == null)
            {
                return NotFound();
            }

            return View(game_Personnel);
        }

        // GET: GamePersonnel/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: GamePersonnel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,GameId,RoleId,Since,Until,Id")] Game_Personnel game_Personnel)
        {
            if (ModelState.IsValid)
            {
                game_Personnel.Id = Guid.NewGuid();
                _context.Add(game_Personnel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Personnel.GameId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", game_Personnel.PersonId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", game_Personnel.RoleId);
            return View(game_Personnel);
        }

        // GET: GamePersonnel/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Personnel = await _context.GamePersonnels.FindAsync(id);
            if (game_Personnel == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Personnel.GameId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", game_Personnel.PersonId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", game_Personnel.RoleId);
            return View(game_Personnel);
        }

        // POST: GamePersonnel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PersonId,GameId,RoleId,Since,Until,Id")] Game_Personnel game_Personnel)
        {
            if (id != game_Personnel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game_Personnel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Game_PersonnelExists(game_Personnel.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Personnel.GameId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", game_Personnel.PersonId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", game_Personnel.RoleId);
            return View(game_Personnel);
        }

        // GET: GamePersonnel/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Personnel = await _context.GamePersonnels
                .Include(g => g.Game)
                .Include(g => g.Person)
                .Include(g => g.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Personnel == null)
            {
                return NotFound();
            }

            return View(game_Personnel);
        }

        // POST: GamePersonnel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game_Personnel = await _context.GamePersonnels.FindAsync(id);
            _context.GamePersonnels.Remove(game_Personnel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Game_PersonnelExists(Guid id)
        {
            return _context.GamePersonnels.Any(e => e.Id == id);
        }
    }
}
