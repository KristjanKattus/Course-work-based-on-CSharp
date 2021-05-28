using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels.ClubTeam;
using Club = BLL.App.DTO.Club;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClubTeamController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.ClubTeamMapper _clubTeamMapper;

        public ClubTeamController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _clubTeamMapper = new PublicApi.DTO.v1.Mappers.ClubTeamMapper(mapper);
        }

        // GET: ClubTeam
        public async Task<IActionResult> Index()
        {
            return View((await _bll.ClubTeams.GetAllAsync()).Select(x => _clubTeamMapper.Map(x)).ToList());
        }

        // GET: ClubTeam/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubTeam = await _bll.ClubTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (clubTeam == null)
            {
                return NotFound();
            }

            return View(_clubTeamMapper.Map(clubTeam));
        }

        // GET: ClubTeam/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ClubTeamCreateEditViewModel();
            vm.ClubSelectList = new SelectList(await _bll.Clubs.GetAllAsync(User.GetUserId()!.Value), nameof(Club.Id),
                nameof(Club.Name));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value), nameof(Team.Id),
                nameof(Team.Name));
            return View(vm);
        }

        // POST: ClubTeam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClubTeamCreateEditViewModel vm)
        {
            
            if (ModelState.IsValid)
            {
                _bll.ClubTeams.Add(_clubTeamMapper.Map(vm.ClubTeam)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ClubSelectList = new SelectList(await _bll.Clubs.GetAllAsync(User.GetUserId()!.Value), nameof(Club.Id),
                nameof(Club.Name));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value), nameof(Team.Id),
                nameof(Team.Name));
            return View(vm);
        }

        // GET: ClubTeam/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubTeam = _clubTeamMapper.Map(await _bll.ClubTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value));
            if (clubTeam == null)
            {
                return NotFound();
            }

            var vm = new ClubTeamCreateEditViewModel();
            vm.ClubTeam = clubTeam;
            vm.ClubSelectList = new SelectList(await _bll.Clubs.GetAllAsync(User.GetUserId()!.Value), nameof(Club.Id),
                nameof(Club.Name));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value), nameof(Team.Id),
                nameof(Team.Name));
            return View(vm);
        }

        // POST: ClubTeam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClubTeamCreateEditViewModel vm)
        {
            if (id == vm.ClubTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.ClubTeams.Update(_clubTeamMapper.Map(vm.ClubTeam)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ClubSelectList = new SelectList(await _bll.Clubs.GetAllAsync(User.GetUserId()!.Value), nameof(Club.Id),
                nameof(Club.Name));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value), nameof(Team.Id),
                nameof(Team.Name));
            return View(vm);
        }

        // GET: ClubTeam/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubTeam = _clubTeamMapper.Map(await _bll.ClubTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value));
            if (clubTeam == null)
            {
                return NotFound();
            }

            return View(clubTeam);
        }

        // POST: ClubTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var clubTeam = await _bll.ClubTeams.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            _bll.ClubTeams.Remove(clubTeam!, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
