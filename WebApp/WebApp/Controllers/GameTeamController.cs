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
    public class GameTeamController : Controller
    {
        private readonly AppDbContext _context;

        public GameTeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GameTeam
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GameTeams.Include(g => g.Game).Include(g => g.Team);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GameTeam/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Team = await _context.GameTeams
                .Include(g => g.Game)
                .Include(g => g.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Team == null)
            {
                return NotFound();
            }

            return View(game_Team);
        }

        // GET: GameTeam/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: GameTeam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,TeamId,Since,Until,Scored,Conceded,Points,Id")] Game_Team game_Team)
        {
            if (ModelState.IsValid)
            {
                game_Team.Id = Guid.NewGuid();
                _context.Add(game_Team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Team.GameId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", game_Team.TeamId);
            return View(game_Team);
        }

        // GET: GameTeam/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Team = await _context.GameTeams.FindAsync(id);
            if (game_Team == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Team.GameId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", game_Team.TeamId);
            return View(game_Team);
        }

        // POST: GameTeam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GameId,TeamId,Since,Until,Scored,Conceded,Points,Id")] Game_Team game_Team)
        {
            if (id != game_Team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game_Team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Game_TeamExists(game_Team.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Team.GameId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", game_Team.TeamId);
            return View(game_Team);
        }

        // GET: GameTeam/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Team = await _context.GameTeams
                .Include(g => g.Game)
                .Include(g => g.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Team == null)
            {
                return NotFound();
            }

            return View(game_Team);
        }

        // POST: GameTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game_Team = await _context.GameTeams.FindAsync(id);
            _context.GameTeams.Remove(game_Team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Game_TeamExists(Guid id)
        {
            return _context.GameTeams.Any(e => e.Id == id);
        }
    }
}
