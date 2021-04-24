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
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels.GamePart;

namespace WebApp.Controllers
{
    public class GamePartController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GamePartMapper _gamePartMapper;

        public GamePartController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gamePartMapper = new GamePartMapper(mapper);
        }

        // GET: GamePart
        public async Task<IActionResult> Index()
        {
            return View((await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gamePartMapper.Map(x)).ToList());
        }

        // GET: GamePart/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePart = await _bll.GameParts.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePart == null)
            {
                return NotFound();
            }

            return View(_gamePartMapper.Map(gamePart));
        }

        // GET: GamePart/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GamePartCreateEditViewModel
            {
                GamePartTypesSelectList = new SelectList(await _bll.GamePartTypes.GetAllAsync(User.GetUserId()!.Value),
                    nameof(GamePartType.Id))
            };
            return View(vm);
        }

        // POST: GamePart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GamePartCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var gamePart = _bll.GameParts.Add(_gamePartMapper.Map(vm.GamePart)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.GamePartTypesSelectList = new SelectList(await _bll.GamePartTypes.GetAllAsync(User.GetUserId()!.Value),
                nameof(GamePartType.Id));
            return View(vm);
        }

        // GET: GamePart/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePart = await _bll.GameParts.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePart == null)
            {
                return NotFound();
            }
            var vm = new GamePartCreateEditViewModel
            {
                GamePart = _gamePartMapper.Map(gamePart)!,
                GamePartTypesSelectList = new SelectList(await _bll.GamePartTypes.GetAllAsync(User.GetUserId()!.Value),
                    nameof(GamePartType.Id))
            };
            return View(vm);
        }

        // POST: GamePart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GamePartCreateEditViewModel vm)
        {
            if (id != vm.GamePart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GameParts.Update(_gamePartMapper.Map(vm.GamePart)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.GamePartTypesSelectList = new SelectList(await _bll.GamePartTypes.GetAllAsync(User.GetUserId()!.Value),
                nameof(GamePartType.Id));
            return View(vm);
        }

        // GET: GamePart/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePart = await _bll.GameParts.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePart == null)
            {
                return NotFound();
            }

            return View(_gamePartMapper.Map(gamePart));
        }

        // POST: GamePart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GameParts.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
