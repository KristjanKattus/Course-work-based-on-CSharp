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
    public class TeamPersonController : Controller
    {
        private readonly AppDbContext _context;

        public TeamPersonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TeamPerson
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TeamPersons.Include(t => t.Person).Include(t => t.Role).Include(t => t.Team);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TeamPerson/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team_Person = await _context.TeamPersons
                .Include(t => t.Person)
                .Include(t => t.Role)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team_Person == null)
            {
                return NotFound();
            }

            return View(team_Person);
        }

        // GET: TeamPerson/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: TeamPerson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,TeamId,RoleId,Id")] Team_Person team_Person)
        {
            if (ModelState.IsValid)
            {
                team_Person.Id = Guid.NewGuid();
                _context.Add(team_Person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", team_Person.PersonId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", team_Person.RoleId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", team_Person.TeamId);
            return View(team_Person);
        }

        // GET: TeamPerson/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team_Person = await _context.TeamPersons.FindAsync(id);
            if (team_Person == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", team_Person.PersonId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", team_Person.RoleId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", team_Person.TeamId);
            return View(team_Person);
        }

        // POST: TeamPerson/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PersonId,TeamId,RoleId,Id")] Team_Person team_Person)
        {
            if (id != team_Person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team_Person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Team_PersonExists(team_Person.Id))
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
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", team_Person.PersonId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", team_Person.RoleId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", team_Person.TeamId);
            return View(team_Person);
        }

        // GET: TeamPerson/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team_Person = await _context.TeamPersons
                .Include(t => t.Person)
                .Include(t => t.Role)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team_Person == null)
            {
                return NotFound();
            }

            return View(team_Person);
        }

        // POST: TeamPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var team_Person = await _context.TeamPersons.FindAsync(id);
            _context.TeamPersons.Remove(team_Person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Team_PersonExists(Guid id)
        {
            return _context.TeamPersons.Any(e => e.Id == id);
        }
    }
}
