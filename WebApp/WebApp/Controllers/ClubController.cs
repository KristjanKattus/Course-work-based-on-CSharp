using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.Controllers
{
    [Authorize]
    public class ClubController : Controller
    {
        
        private readonly IAppUnitOfWork _uow;

        public ClubController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Club
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Clubs.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: Club/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _uow.Clubs
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        // GET: Club/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Club/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Since,Until,Description,Id")]
            Club club)
        {
            if (ModelState.IsValid)
            {
                club.Id = Guid.NewGuid();
                _uow.Clubs.Add(club);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(club);
        }

        // GET: Club/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _uow.Clubs.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        // POST: Club/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Since,Until,Description,Id")]
            Club club)
        {
            if (id != club.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(club);
            _uow.Clubs.Update(club);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Club/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _uow.Clubs
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        // POST: Club/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var club = await _uow.Clubs.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            _uow.Clubs.Remove(club!, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}