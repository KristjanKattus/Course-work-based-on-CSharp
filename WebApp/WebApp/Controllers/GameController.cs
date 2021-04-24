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
using WebApp.ViewModels.Club;
using WebApp.ViewModels.Game;
using Stadium = BLL.App.DTO.Stadium;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GameMapper _gameMapper;

        public GameController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gameMapper = new GameMapper(mapper);

        }

        // GET: Game
        public async Task<IActionResult> Index()
        {
            
            return View((await _bll.Games.GetAllAsync(User.GetUserId()!.Value)).Select(x => _gameMapper.Map(x)).ToList());
        }

        // GET: Game/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(_gameMapper.Map(game));
        }

        // GET: Game/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GameCreateEditViewModel
            {
                Stadiums = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value),
                    nameof(Stadium.Id))
            };

            return View(vm);
        }

        // POST: Game/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Games.Add(_gameMapper.Map(vm.Game)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.Stadiums = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value), nameof(Stadium.Id));
            return View(vm);
        }

        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (game == null)
            {
                return NotFound();
            }

            var vm = new GameCreateEditViewModel
            {
                Game = _gameMapper.Map(game)!,
                Stadiums = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value),
                    nameof(Stadium.Id))
            };
            return View(vm);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameCreateEditViewModel vm)
        {
            if (id != vm.Game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Games.Update(_gameMapper.Map(vm.Game)!);
                await _bll.SaveChangesAsync();
;                return RedirectToAction(nameof(Index));
            }
            vm.Stadiums = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value), nameof(Stadium.Id));
            return View(vm);
        }

        // GET: Game/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(_gameMapper.Map(game)!);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Games.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
