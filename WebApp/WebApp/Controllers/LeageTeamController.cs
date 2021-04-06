using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using WebApp.ViewModels.LeagueTeam;

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
            var vm = new LeagueTeamCreateEditViewModel
            {
                LeaguesSelectList = new SelectList(_context.Leagues, "Id", "Name"),
                TeamsSelectList = new SelectList(_context.Teams, "Id", "Name")
            };

            return View(vm);
        }

        // POST: LeageTeam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueTeamCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.LeagueTeam.Id = Guid.NewGuid();
                _context.Add(vm.LeagueTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.LeaguesSelectList = new SelectList(_context.Leagues, "Id", "Name", vm.LeagueTeam.LeagueId);
            vm.LeaguesSelectList = new SelectList(_context.Teams, "Id", "Name", vm.LeagueTeam.TeamId);
            return View(vm);
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
            var vm = new LeagueTeamCreateEditViewModel
            {
                LeagueTeam = league_Team,
                LeaguesSelectList = new SelectList(_context.Leagues, "Id", "Name"),
                TeamsSelectList = new SelectList(_context.Teams, "Id", "Name")
            };
            return View(vm);
        }

        // POST: LeageTeam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, LeagueTeamCreateEditViewModel vm)
        {
            if (id != vm.LeagueTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vm.LeagueTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!League_TeamExists(vm.LeagueTeam.Id))
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
            vm.LeaguesSelectList = new SelectList(_context.Leagues, "Id", "Name", vm.LeagueTeam.LeagueId);
            vm.TeamsSelectList = new SelectList(_context.Teams, "Id", "Name", vm.LeagueTeam.TeamId);
            return View(vm);
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
