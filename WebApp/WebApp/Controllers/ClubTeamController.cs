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
    public class ClubTeamController : Controller
    {
        private readonly AppDbContext _context;

        public ClubTeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ClubTeam
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ClubTeams.Include(c => c.Club).Include(c => c.Team);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ClubTeam/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club_Team = await _context.ClubTeams
                .Include(c => c.Club)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club_Team == null)
            {
                return NotFound();
            }

            return View(club_Team);
        }

        // GET: ClubTeam/Create
        public IActionResult Create()
        {
            ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: ClubTeam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,ClubId,Since,Until,Description,Id")] Club_Team club_Team)
        {
            if (ModelState.IsValid)
            {
                club_Team.Id = Guid.NewGuid();
                _context.Add(club_Team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "Name", club_Team.ClubId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", club_Team.TeamId);
            return View(club_Team);
        }

        // GET: ClubTeam/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club_Team = await _context.ClubTeams.FindAsync(id);
            if (club_Team == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "Name", club_Team.ClubId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", club_Team.TeamId);
            return View(club_Team);
        }

        // POST: ClubTeam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TeamId,ClubId,Since,Until,Description,Id")] Club_Team club_Team)
        {
            if (id != club_Team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(club_Team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Club_TeamExists(club_Team.Id))
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
            ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "Name", club_Team.ClubId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", club_Team.TeamId);
            return View(club_Team);
        }

        // GET: ClubTeam/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club_Team = await _context.ClubTeams
                .Include(c => c.Club)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club_Team == null)
            {
                return NotFound();
            }

            return View(club_Team);
        }

        // POST: ClubTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var club_Team = await _context.ClubTeams.FindAsync(id);
            _context.ClubTeams.Remove(club_Team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Club_TeamExists(Guid id)
        {
            return _context.ClubTeams.Any(e => e.Id == id);
        }
    }
}
