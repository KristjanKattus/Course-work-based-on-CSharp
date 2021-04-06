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
    public class GameTeamListController : Controller
    {
        private readonly AppDbContext _context;

        public GameTeamListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GameTeamList
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GameTeamListMembers.Include(g => g.GameTeam).Include(g => g.Person).Include(g => g.Role);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GameTeamList/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Team_List = await _context.GameTeamListMembers
                .Include(g => g.GameTeam)
                .Include(g => g.Person)
                .Include(g => g.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Team_List == null)
            {
                return NotFound();
            }

            return View(game_Team_List);
        }

        // GET: GameTeamList/Create
        public IActionResult Create()
        {
            ViewData["GameTeamId"] = new SelectList(_context.GameTeams, "Id", "Id");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["RoleId"] = new SelectList(_context.FRoles, "Id", "Name");
            return View();
        }

        // POST: GameTeamList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,GameTeamId,RoleId,InStartingLineup,Staff,Id")] Game_Team_List game_Team_List)
        {
            if (ModelState.IsValid)
            {
                game_Team_List.Id = Guid.NewGuid();
                _context.Add(game_Team_List);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameTeamId"] = new SelectList(_context.GameTeams, "Id", "Id", game_Team_List.GameTeamId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", game_Team_List.PersonId);
            ViewData["RoleId"] = new SelectList(_context.FRoles, "Id", "Name", game_Team_List.RoleId);
            return View(game_Team_List);
        }

        // GET: GameTeamList/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Team_List = await _context.GameTeamListMembers.FindAsync(id);
            if (game_Team_List == null)
            {
                return NotFound();
            }
            ViewData["GameTeamId"] = new SelectList(_context.GameTeams, "Id", "Id", game_Team_List.GameTeamId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", game_Team_List.PersonId);
            ViewData["RoleId"] = new SelectList(_context.FRoles, "Id", "Name", game_Team_List.RoleId);
            return View(game_Team_List);
        }

        // POST: GameTeamList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PersonId,GameTeamId,RoleId,InStartingLineup,Staff,Id")] Game_Team_List game_Team_List)
        {
            if (id != game_Team_List.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game_Team_List);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Game_Team_ListExists(game_Team_List.Id))
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
            ViewData["GameTeamId"] = new SelectList(_context.GameTeams, "Id", "Id", game_Team_List.GameTeamId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", game_Team_List.PersonId);
            ViewData["RoleId"] = new SelectList(_context.FRoles, "Id", "Name", game_Team_List.RoleId);
            return View(game_Team_List);
        }

        // GET: GameTeamList/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game_Team_List = await _context.GameTeamListMembers
                .Include(g => g.GameTeam)
                .Include(g => g.Person)
                .Include(g => g.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game_Team_List == null)
            {
                return NotFound();
            }

            return View(game_Team_List);
        }

        // POST: GameTeamList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game_Team_List = await _context.GameTeamListMembers.FindAsync(id);
            _context.GameTeamListMembers.Remove(game_Team_List);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Game_Team_ListExists(Guid id)
        {
            return _context.GameTeamListMembers.Any(e => e.Id == id);
        }
    }
}
