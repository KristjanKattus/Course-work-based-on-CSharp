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
using WebApp.ViewModels.GameTeam;

namespace WebApp.Controllers
{
    public class GameTeamController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GameTeamMapper _gameTeamMapper;

        public GameTeamController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gameTeamMapper = new GameTeamMapper(mapper);
        }

        // GET: GameTeam
        public async Task<IActionResult> Index()
        {
            
            return View((await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gameTeamMapper.Map(x)).ToList());
        }

        // GET: GameTeam/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTeam = await _bll.GameTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameTeam == null)
            {
                return NotFound();
            }

            return View(_gameTeamMapper.Map(gameTeam));
        }

        // GET: GameTeam/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GameTeamCreateEditViewModel
            {
                GameSelectList = new SelectList(await _bll.Games.GetAllAsync(User.GetUserId()!.Value)),
                TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value))
            };
            
            return View(vm);
        }

        // POST: GameTeam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameTeamCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.GameTeams.Add(_gameTeamMapper.Map(vm.GameTeam)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.GameSelectList = new SelectList(await _bll.Games.GetAllAsync(User.GetUserId()!.Value));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value));
            
            return View(vm);
        }

        // GET: GameTeam/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTeam = await _bll.GameTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameTeam == null)
            {
                return NotFound();
            }
            var vm = new GameTeamCreateEditViewModel
            {
                GameTeam = _gameTeamMapper.Map(gameTeam)!,
                GameSelectList = new SelectList(await _bll.Games.GetAllAsync(User.GetUserId()!.Value)),
                TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value))
            };
            return View(vm);
        }

        // POST: GameTeam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameTeamCreateEditViewModel vm)
        {
            if (id != vm.GameTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GameTeams.Update(_gameTeamMapper.Map(vm.GameTeam)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.GameSelectList = new SelectList(await _bll.Games.GetAllAsync(User.GetUserId()!.Value));
            vm.TeamSelectList = new SelectList(await _bll.Teams.GetAllAsync(User.GetUserId()!.Value));
            return View(vm);
        }

        // GET: GameTeam/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTeam = await _bll.GameTeams.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameTeam == null)
            {
                return NotFound();
            }

            return View(_gameTeamMapper.Map(gameTeam));
        }

        // POST: GameTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GameTeams.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
