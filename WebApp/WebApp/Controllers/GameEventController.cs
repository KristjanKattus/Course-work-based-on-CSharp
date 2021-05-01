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
using WebApp.ViewModels.GameEvent;

namespace WebApp.Controllers
{
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
        public async Task<IActionResult> Create()
        {
            var vm = new GameEventCreateEditViewModel
            {
                EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)),
                GameSelectList = new SelectList(await _bll.Games.GetAllAsync(User.GetUserId()!.Value)),
                GamePartSelectList = new SelectList(await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value)),
                GamePersonnelSelectList = new SelectList(await _bll.GamePersonnel.GetAllAsync(User.GetUserId()!.Value)),
                GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value))
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

                _bll.GameEvents.Add(_gameEventMapper.Map(vm.GameEvent)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value));
            vm.GameSelectList = new SelectList(await _bll.Games.GetAllAsync(User.GetUserId()!.Value));
            vm.GamePartSelectList = new SelectList(await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value));
            vm.GamePersonnelSelectList = new SelectList(await _bll.GamePersonnel.GetAllAsync(User.GetUserId()!.Value));
            vm.GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value));
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
                EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)),
                GameSelectList = new SelectList(await _bll.Games.GetAllAsync(User.GetUserId()!.Value)),
                GamePartSelectList = new SelectList(await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value)),
                GamePersonnelSelectList = new SelectList(await _bll.GamePersonnel.GetAllAsync(User.GetUserId()!.Value)),
                GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value))
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
            vm.EventTypeSelectList = new SelectList(await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value));
            vm.GameSelectList = new SelectList(await _bll.Games.GetAllAsync(User.GetUserId()!.Value));
            vm.GamePartSelectList = new SelectList(await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value));
            vm.GamePersonnelSelectList = new SelectList(await _bll.GamePersonnel.GetAllAsync(User.GetUserId()!.Value));
            vm.GameTeamSelectList = new SelectList(await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value));
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
