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
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels.LeagueTeam;

namespace WebApp.Controllers
{
    
    [Authorize]
    public class LeagueTeamController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.LeagueTeamMapper _leagueTeamMapper;
        
        public LeagueTeamController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _leagueTeamMapper = new LeagueTeamMapper(mapper);
        }

        // GET: LeageTeam
        public async Task<IActionResult> Index()
        {
            return View((await _bll.LeagueTeams.GetAllAsync()).Select(x => _leagueTeamMapper.Map(x)).ToList());
        }

        // GET: LeageTeam/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTeam = await _bll.LeagueTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (leagueTeam == null)
            {
                return NotFound();
            }

            return View(_leagueTeamMapper.Map(leagueTeam));
        }

        // GET: LeageTeam/Create
        public async Task<IActionResult> Create()
        {
            var vm = new LeagueTeamCreateEditViewModel
            {
                LeagueSelectList = new SelectList(await _bll.Leagues.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(League.Id),  nameof(League.Name)),
                TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(Team.Id), nameof(Team.Name))
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
                _bll.LeagueTeams.Add(_leagueTeamMapper.Map(vm.LeagueTeam)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.LeagueSelectList = new SelectList(await _bll.Leagues.GetAllAsync(User.GetUserId()!.Value)
                , nameof(League.Id),  nameof(League.Name));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value)
                , nameof(Team.Id), nameof(Team.Name));
            return View(vm);
        }

        // GET: LeageTeam/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTeam = await _bll.LeagueTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (leagueTeam == null)
            {
                return NotFound();
            }
            var vm = new LeagueTeamCreateEditViewModel
            {
                LeagueTeam = _leagueTeamMapper.Map(leagueTeam)!,
                LeagueSelectList = new SelectList(await _bll.Leagues.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(League.Id),  nameof(League.Name)),
                TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(Team.Id), nameof(Team.Name))
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
                _bll.LeagueTeams.Update(_leagueTeamMapper.Map(vm.LeagueTeam)!);
                return RedirectToAction(nameof(Index));
            }
            vm.LeagueSelectList = new SelectList(await _bll.Leagues.GetAllAsync(User.GetUserId()!.Value)
                , nameof(League.Id),  nameof(League.Name));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value)
                , nameof(Team.Id), nameof(Team.Name));
            return View(vm);
        }

        // GET: LeageTeam/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTeam = await _bll.LeagueTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (leagueTeam == null)
            {
                return NotFound();
            }

            return View(_leagueTeamMapper.Map(leagueTeam));
        }

        // POST: LeageTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.LeagueTeams.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
