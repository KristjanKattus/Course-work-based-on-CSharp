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
using WebApp.ViewModels.GamePartType;

namespace WebApp.Controllers
{
    public class GamePersonnelController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GamePersonnelMapper _gamePersonnelMapper;

        public GamePersonnelController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _gamePersonnelMapper = new GamePersonnelMapper(mapper);
            
        }

        // GET: GamePersonnel
        public async Task<IActionResult> Index()
        {
            return View((await _bll.GamePersonnel.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gamePersonnelMapper.Map(x)).ToList());
        }

        // GET: GamePersonnel/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePersonnel = await _bll.GamePersonnel.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePersonnel == null)
            {
                return NotFound();
            }

            return View(_gamePersonnelMapper.Map(gamePersonnel));
        }

        // GET: GamePersonnel/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GamePersonnelCreateEditViewModel
            {
                Persons = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value)),
                Roles = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value))
            };
            
            return View(vm);
        }

        // POST: GamePersonnel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GamePersonnelCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.GamePersonnel.Add(_gamePersonnelMapper.Map(vm.GamePersonnel)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Personnel.GameId);
            vm.Persons = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value));
            vm.Roles = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value));
            return View(vm);
        }

        // GET: GamePersonnel/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePersonnel = await _bll.GamePersonnel.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePersonnel == null)
            {
                return NotFound();
            }
            // ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", game_Personnel.GameId);
            var vm = new GamePersonnelCreateEditViewModel
            {
                GamePersonnel = _gamePersonnelMapper.Map(gamePersonnel)!,
                Persons = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value)),
                Roles = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value))
            };
            return View(vm);
        }

        // POST: GamePersonnel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GamePersonnelCreateEditViewModel vm)
        {
            if (id != vm.GamePersonnel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GamePersonnel.Update(_gamePersonnelMapper.Map(vm.GamePersonnel)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Persons = new SelectList(await _bll.Persons.GetAllAsync(User.GetUserId()!.Value));
            vm.Roles = new SelectList(await _bll.Roles.GetAllAsync(User.GetUserId()!.Value));
            return View(vm);
        }

        // GET: GamePersonnel/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePersonnel = await _bll.GamePersonnel.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePersonnel == null)
            {
                return NotFound();
            }

            return View(_gamePersonnelMapper.Map(gamePersonnel));
        }

        // POST: GamePersonnel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            await _bll.GamePersonnel.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
