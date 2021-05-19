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
using WebApp.ViewModels.TeamPerson;

namespace WebApp.Controllers
{
    [Authorize]
    public class TeamPersonController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.TeamPersonMapper _teamPersonMapper;

        public TeamPersonController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _teamPersonMapper = new TeamPersonMapper(mapper);
        }

        // GET: TeamPerson
        public async Task<IActionResult> Index()
        {
            
            return View((await _bll.TeamPersons.GetAllAsync())
                .Select(x => _teamPersonMapper.Map(x)).ToList());
        }

        // GET: TeamPerson/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPerson = await _bll.TeamPersons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (teamPerson == null)
            {
                return NotFound();
            }

            return View(_teamPersonMapper.Map(teamPerson));
        }

        // GET: TeamPerson/Create
        public async Task<IActionResult> Create()
        {
            var vm = new TeamPersonCreateEditViewModel
            {
                PersonSelectList = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value)),
                RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value)),
                TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value))
            };
            return View(vm);
        }

        // POST: TeamPerson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamPersonCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.TeamPersons.Add(_teamPersonMapper.Map(vm.TeamPerson)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.PersonSelectList = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value));
            vm.RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value));
            return View(vm);
        }

        // GET: TeamPerson/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPerson = await _bll.TeamPersons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (teamPerson == null)
            {
                return NotFound();
            }
            var vm = new TeamPersonCreateEditViewModel
            {
                TeamPerson = _teamPersonMapper.Map(teamPerson)!,
                PersonSelectList = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value)),
                RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value)),
                TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value))
            };
            return View(vm);
        }

        // POST: TeamPerson/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TeamPersonCreateEditViewModel vm)
        {
            if (id != vm.TeamPerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.TeamPersons.Update(_teamPersonMapper.Map(vm.TeamPerson)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.PersonSelectList = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value));
            vm.RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value));
            return View(vm);
        }

        // GET: TeamPerson/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPerson = await _bll.TeamPersons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (teamPerson == null)
            {
                return NotFound();
            }

            return View(_teamPersonMapper.Map(teamPerson));
        }

        // POST: TeamPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.TeamPersons.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
