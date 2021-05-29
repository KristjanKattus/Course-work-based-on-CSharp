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
using Person = Domain.App.Person;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin, Referee")]
    public class GameEventController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;
        private readonly PublicApi.DTO.v1.Mappers.GameEventMapper _gameEventMapper;

        public GameEventController(IMapper mapper, IAppBLL bll)
        {
            _mapper = mapper;
            _bll = bll;
            _gameEventMapper = new GameEventMapper(mapper);
        }

        // GET: GameEvent
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View((await _bll.GameEvents.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gameEventMapper.Map(x)).ToList());
        }

        // GET: GameEvent/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin, Referee")]
        public async Task<IActionResult> Create(Guid gameTeamId)
        {

            var gameTeam = await _bll.GameTeams.FirstOrDefaultAsync(gameTeamId);
            var game = await _bll.Games.FirstOrDefaultAsync(gameTeam!.GameId);

            var gameTeams = (await _bll.GameTeams.GetAllAsync(game.Id)).ToList();
            var vm = new GameEventCreateEditViewModel
            {
                HomeTeamId = gameTeamId,
                AwayTeamId = (gameTeams.FirstOrDefault(x=> x.Id != gameTeamId)!).Id,
                GameId = game.Id,
                EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)
                , nameof(EventType.Id), nameof(EventType.Name)),
                GameTeamListSelectList = new SelectList(await _bll.GameTeamLists.GetAllWithLeagueTeamIdAsync(gameTeamId)
                    , nameof(GameTeamList.Id), nameof(GameTeamList.TeamPerson))
            };
            return View(vm);
        }

        // POST: GameEvent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Referee")]
        public async Task<IActionResult> Create(GameEventCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                
                vm.GameEvent.GameId = vm.GameId;
                var gameEvent = _bll.GameEvents.Add(_gameEventMapper.Map(vm.GameEvent)!);
                
                await _bll.SaveChangesAsync();
                await _bll.GameTeams.UpdateEntity(gameEvent.Id);
                return RedirectToAction("Details", "Game", new{id = gameEvent.GameId});
            }

            vm.HomeTeamId = vm.HomeTeamId;
            vm.GameId = vm.GameId;
            vm.EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)
                , nameof(EventType.Id), nameof(EventType.Name));

            vm.GameTeamListSelectList = new SelectList(await _bll.GameTeamLists.GetAllWithLeagueTeamIdAsync(vm.HomeTeamId)
                , nameof(GameTeam.Id), nameof(GameTeamList.Person));
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

            var game = await _bll.Games.FirstOrDefaultAsync(gameEvent.GameId);
            var gameTeam = await _bll.GameTeams.FirstOrDefaultAsync(game.Id);

            var gameTeams = (await _bll.GameTeams.GetAllAsync(game.Id)).ToList();
            var vm = new GameEventCreateEditViewModel
            {
                AwayTeamId = (gameTeams.FirstOrDefault(x=> x.Id != gameTeam!.Id)!).Id,
                HomeTeamId = gameTeam!.Id,
                GameEvent = _gameEventMapper.Map(gameEvent)!,
                GameId = gameTeam!.Id,
                EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(EventType.Id), nameof(EventType.Name)),
                GameTeamListSelectList = new SelectList(await _bll.GameTeamLists.GetAllWithLeagueTeamIdAsync(gameTeam.Id)
                    , nameof(GameTeam.Id), nameof(GameTeamList.Person))
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
            
            var game = await _bll.Games.FirstOrDefaultAsync(vm.GameId);
            var gameTeam = await _bll.GameTeams.FirstOrDefaultAsync(vm.HomeTeamId);

            var gameTeams = (await _bll.GameTeams.GetAllAsync(game.Id)).ToList();
            vm.AwayTeamId = (gameTeams.FirstOrDefault(x => x.Id != gameTeam!.Id)!).Id;
            vm.HomeTeamId = vm.HomeTeamId;
            vm.GameId = vm.GameId;
            vm.EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)
                , nameof(EventType.Id), nameof(EventType.Name));

            vm.GameTeamListSelectList = new SelectList(await _bll.GameTeamLists.GetAllWithLeagueTeamIdAsync(vm.HomeTeamId)
                , nameof(GameTeam.Id), nameof(GameTeamList.Person));
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
            await _bll.GameEvents.DeleteGameEventAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
