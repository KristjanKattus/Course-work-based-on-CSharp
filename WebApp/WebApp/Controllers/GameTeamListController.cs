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
using WebApp.ViewModels.GameTeamList;

namespace WebApp.Controllers
{
    [Authorize]
    public class GameTeamListController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GameTeamListMapper _gameTeamListMapper;

        public GameTeamListController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gameTeamListMapper = new GameTeamListMapper(mapper);
        }

        // GET: GameTeamList
        public async Task<IActionResult> Index()
        {
            return View((await _bll.GameTeamLists.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gameTeamListMapper.Map(x)).ToList());
        }

        // GET: GameTeamList/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTeamList = await _bll.GameTeamLists.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameTeamList == null)
            {
                return NotFound();
            }

            return View(_gameTeamListMapper.Map(gameTeamList));
        }

        // GET: GameTeamList/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GameTeamListCreateEditViewModel
            {
                GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value)),
                PersonSelectList = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value)),
                RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value))
                
            };
            
            return View(vm);
        }

        // POST: GameTeamList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameTeamListCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.GameTeamLists.Add(_gameTeamListMapper.Map(vm.GameTeamList)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value));
            vm.PersonSelectList = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value));
            vm.RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value));
            
            return View(vm);
        }

        // GET: GameTeamList/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTeamList = await _bll.GameTeamLists.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameTeamList == null)
            {
                return NotFound();
            }
            var vm = new GameTeamListCreateEditViewModel
            {
                GameTeamList = _gameTeamListMapper.Map(gameTeamList)!,
                GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value)),
                PersonSelectList = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value)),
                RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value))
                
            };
            return View(vm);
        }

        // POST: GameTeamList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameTeamListCreateEditViewModel vm)
        {
            if (id != vm.GameTeamList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GameTeamLists.Update(_gameTeamListMapper.Map(vm.GameTeamList)!);
                return RedirectToAction(nameof(Index));
            }
            vm.GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value));
            vm.PersonSelectList = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value));
            vm.RoleSelectList = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value));
            return View(vm);
        }

        // GET: GameTeamList/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTeamList = await _bll.GameTeamLists.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameTeamList == null)
            {
                return NotFound();
            }

            return View(_gameTeamListMapper.Map(gameTeamList));
        }

        // POST: GameTeamList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GameTeamLists.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
