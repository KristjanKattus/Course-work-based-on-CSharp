using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.Controllers
{
    [Authorize]
    public class AreaController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public AreaController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Stadium_Area
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Areas.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: Stadium_Area/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _uow.Areas
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Stadium_Area/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stadium_Area/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Since,Until,Id")] Stadium_Area stadiumArea)
        {
            if (ModelState.IsValid)
            {
                stadiumArea.Id = Guid.NewGuid();
                _uow.Areas.Add(stadiumArea);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(stadiumArea);
        }

        // GET: Stadium_Area/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _uow.Areas.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // POST: Stadium_Area/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Stadium_Area stadiumArea)
        {
            if (id != stadiumArea.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(stadiumArea);

            _uow.Areas.Update(stadiumArea);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Stadium_Area/Delete/5
            public async Task<IActionResult> Delete(Guid? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var area = await _uow.Areas
                    .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
                if (area == null)
                {
                    return NotFound();
                }

                return View(area);
            }

            // POST: Stadium_Area/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
                var area = await _uow.Areas.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
                _uow.Areas.Remove(area!, User.GetUserId()!.Value);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }