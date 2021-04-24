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
using WebApp.ViewModels.League;

namespace WebApp.Controllers
{
    public class LeagueController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.LeagueMapper _leagueMapper;

        public LeagueController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _leagueMapper = new LeagueMapper(mapper);
        }

        // GET: League
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Leagues.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _leagueMapper.Map(x)).ToList());
        }

        // GET: League/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = await _bll.Leagues.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (league == null)
            {
                return NotFound();
            }

            return View(_leagueMapper.Map(league));
        }

        // GET: League/Create
        public IActionResult Create()
        {
            var vm = new LeagueCreateEditViewModel();
            return View(vm);
        }

        // POST: League/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Leagues.Add(_leagueMapper.Map(vm.League)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: League/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = await _bll.Leagues.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (league == null)
            {
                return NotFound();
            }

            var vm = new LeagueCreateEditViewModel {League = _leagueMapper.Map(league)!};

            return View(vm);
        }

        // POST: League/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, LeagueCreateEditViewModel vm)
        {
            if (id != vm.League.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Leagues.Update(_leagueMapper.Map(vm.League)!);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: League/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = await _bll.Leagues.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (league == null)
            {
                return NotFound();
            }

            return View(_leagueMapper.Map(league));
        }

        // POST: League/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Leagues.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
