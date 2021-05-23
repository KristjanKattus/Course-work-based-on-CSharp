using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels.GameEvent;

namespace WebApp.Controllers
{
    [Authorize]
    public class GameEventController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GameEventMapper _gameEventMapper;

        public GameEventController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gameEventMapper = new GameEventMapper(mapper);
        }

        // GET: GameEvent
        public async Task<IActionResult> Index()
        {
            return View((await _bll.GameEvents.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gameEventMapper.Map(x)).ToList());
        }

        // GET: GameEvent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameEvent = await _bll.GameEvents.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameEvent == null)
            {
                return NotFound();
            }

            return View(_gameEventMapper.Map(gameEvent));
        }

        // GET: GameEvent/Create
        public async Task<IActionResult> Create(Guid gameId)
        {
            var vm = new GameEventCreateEditViewModel
            {
                GameId = gameId,
                EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)
                , nameof(EventType.Id), nameof(EventType.Name)),
                GamePartSelectList = new SelectList(await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value)
                , nameof(GamePart.Id), nameof(GamePart.GamePartType.Name)),
                GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllTeamGamesWithGameIdAsync(gameId)
                    , nameof(GameTeam.Id), nameof(GameTeam.TeamName))
            };
            return View(vm);
        }

        // POST: GameEvent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameEventCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.GameEvent.GameId = vm.GameId;
                _bll.GameEvents.Add(_gameEventMapper.Map(vm.GameEvent)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.GameId = vm.GameId;
            vm.EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)
                , nameof(EventType.Id), nameof(EventType.Name));
           
            vm.GamePartSelectList = new SelectList(await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value)
                , nameof(GamePart.Id), nameof(GamePart.GamePartType.Name));
            
            vm.GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllTeamGamesWithGameIdAsync(vm.GameId)
                , nameof(GameTeam.Id), nameof(GameTeam.Team.Name));
            return View(vm);
        }

        // GET: GameEvent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameEvent = await _bll.GameEvents.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameEvent == null)
            {
                return NotFound();
            }
            var vm = new GameEventCreateEditViewModel
            {
                GameEvent = _gameEventMapper.Map(gameEvent)!,
                EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(EventType.Id), nameof(EventType.Name)),
                GamePartSelectList = new SelectList(await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(GamePart.Id), nameof(GamePart.GamePartType.Name)),
                GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllTeamGamesWithGameIdAsync(gameEvent.GameId)
                    , nameof(GameTeam.Id), nameof(GameTeam.Team.Name))
            };
            return View(vm);
        }

        // POST: GameEvent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameEventCreateEditViewModel vm)
        {
            if (id != vm.GameEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GameEvents.Update(_gameEventMapper.Map(vm.GameEvent)!);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            
            vm.EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)
                , nameof(EventType.Id), nameof(EventType.Name));
           
            vm.GamePartSelectList = new SelectList(await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value)
                , nameof(GamePart.Id), nameof(GamePart.GamePartType.Name));
            
            vm.GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value)
                , nameof(GameTeam.Id), nameof(GameTeam.Team.Name));
            return View(vm);
        }

        // GET: GameEvent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameEvent = await _bll.GameEvents.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameEvent == null)
            {
                return NotFound();
            }

            return View(_gameEventMapper.Map(gameEvent));
        }

        // POST: GameEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GameEvents.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
