using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels.Stadium;

namespace WebApp.Controllers
{
    public class StadiumController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.StadiumMapper _stadiumMapper;

        public StadiumController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _stadiumMapper = new StadiumMapper(mapper);
        }

        // GET: Stadium
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _stadiumMapper.Map(x)).ToList());
        }

        // GET: Stadium/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _bll.Stadiums.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(_stadiumMapper.Map(stadium));
        }

        // GET: Stadium/Create
        public async Task<IActionResult> Create()
        {
            var vm = new StadiumCreateEditViewModel
                {StadiumAreaSelectList = new SelectList(await _bll.StadiumAreas.GetAllAsync(User.GetUserId()!.Value))};
            return View(vm);
        }

        // POST: Stadium/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StadiumCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Stadiums.Add(_stadiumMapper.Map(vm.Stadium)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.StadiumAreaSelectList = new SelectList(await _bll.StadiumAreas.GetAllAsync(User.GetUserId()!.Value));
            
            
            return View(vm);
        }

        // GET: Stadium/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _bll.Stadiums.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (stadium == null)
            {
                return NotFound();
            }
            
            var vm = new StadiumCreateEditViewModel
            {
                Stadium = _stadiumMapper.Map(stadium)!,
                StadiumAreaSelectList = new SelectList(await _bll.StadiumAreas.GetAllAsync(User.GetUserId()!.Value))
            };
            
            return View(vm);
        }

        // POST: Stadium/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StadiumCreateEditViewModel vm)
        {
            if (id != vm.Stadium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Stadiums.Update(_stadiumMapper.Map(vm.Stadium)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.StadiumAreaSelectList = new SelectList(await _bll.StadiumAreas.GetAllAsync(User.GetUserId()!.Value));
            return View(vm);
        }

        // GET: Stadium/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _bll.Stadiums.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(_stadiumMapper.Map(stadium));
        }

        // POST: Stadium/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Stadiums.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
