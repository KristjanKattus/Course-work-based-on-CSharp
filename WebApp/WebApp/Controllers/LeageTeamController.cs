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
    public class LeageTeamController : Controller
    {
        private readonly AppDbContext _context;

        public LeageTeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LeageTeam
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.LeagueTeams.Include(l => l.League).Include(l => l.Team);
            return View(await appDbContext.ToListAsync());
        }

        // GET: LeageTeam/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league_Team = await _context.LeagueTeams
                .Include(l => l.League)
                .Include(l => l.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (league_Team == null)
            {
                return NotFound();
            }

            return View(league_Team);
        }

        // GET: LeageTeam/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: LeageTeam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeagueId,TeamId,Since,Until,Description,Id")] League_Team league_Team)
        {
            if (ModelState.IsValid)
            {
                league_Team.Id = Guid.NewGuid();
                _context.Add(league_Team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", league_Team.LeagueId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", league_Team.TeamId);
            return View(league_Team);
        }

        // GET: LeageTeam/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league_Team = await _context.LeagueTeams.FindAsync(id);
            if (league_Team == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", league_Team.LeagueId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", league_Team.TeamId);
            return View(league_Team);
        }

        // POST: LeageTeam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LeagueId,TeamId,Since,Until,Description,Id")] League_Team league_Team)
        {
            if (id != league_Team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(league_Team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!League_TeamExists(league_Team.Id))
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
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", league_Team.LeagueId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", league_Team.TeamId);
            return View(league_Team);
        }

        // GET: LeageTeam/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league_Team = await _context.LeagueTeams
                .Include(l => l.League)
                .Include(l => l.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (league_Team == null)
            {
                return NotFound();
            }

            return View(league_Team);
        }

        // POST: LeageTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var league_Team = await _context.LeagueTeams.FindAsync(id);
            _context.LeagueTeams.Remove(league_Team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool League_TeamExists(Guid id)
        {
            return _context.LeagueTeams.Any(e => e.Id == id);
        }
    }
}
