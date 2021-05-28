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
using WebApp.ViewModels.Team;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;
        private readonly PublicApi.DTO.v1.Mappers.TeamMapper _teamMapper;

        public TeamController(IMapper mapper, IAppBLL bll)
        {
            _mapper = mapper;
            _bll = bll;
            _teamMapper = new TeamMapper(mapper);
        }

        // GET: Team
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Teams.GetAllAsync())
                .Select(x => _teamMapper.Map(x)).ToList());
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _bll.Teams.GetClientTeamAsync(id.Value, _mapper);
            if (team == null)
            {
                return NotFound();
            }

            var clientTeamMapper = new ClientTeamMapper(_mapper);
            
            var vm = new TeamClientViewModel
            {
                Team = clientTeamMapper.Map(team)!
            };

            return View(vm);
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            var vm = new TeamCreateEditViewModel();
            return View(vm);
        }

        // POST: Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Teams.Add(_teamMapper.Map(vm.Team)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _bll.Teams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (team == null)
            {
                return NotFound();
            }

            var vm = new TeamCreateEditViewModel{Team = _teamMapper.Map(team)!};
            return View(vm);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TeamCreateEditViewModel vm)
        {
            if (id != vm.Team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Teams.Update(_teamMapper.Map(vm.Team)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _bll.Teams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            
            if (team == null)
            {
                return NotFound();
            }

            return View(_teamMapper.Map(team));
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Teams.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
