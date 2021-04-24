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
    public class GamePartTypeController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GamePartTypeMapper _gamePartTypeMapper;

        public GamePartTypeController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gamePartTypeMapper = new GamePartTypeMapper(mapper);
            
        }

        // GET: GamePartType
        public async Task<IActionResult> Index()
        {
            return View((await _bll.GamePartTypes.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gamePartTypeMapper.Map(x)).ToList());
        }

        // GET: GamePartType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePartType = await _bll.GamePartTypes.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePartType == null)
            {
                return NotFound();
            }

            return View(_gamePartTypeMapper.Map(gamePartType));
        }

        // GET: GamePartType/Create
        public IActionResult Create()
        {
            var vm = new GamePartTypeCreateEditViewModel();
            return View(vm);
        }

        // POST: GamePartType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GamePartTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.GamePartTypes.Add(_gamePartTypeMapper.Map(vm.GamePartType)!);
                
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: GamePartType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePartType = await _bll.GamePartTypes.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePartType == null)
            {
                return NotFound();
            }

            var vm = new GamePartTypeCreateEditViewModel();
            vm.GamePartType = _gamePartTypeMapper.Map(gamePartType)!;
            return View(vm);
        }

        // POST: GamePartType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GamePartTypeCreateEditViewModel vm)
        {
            if (id != vm.GamePartType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GamePartTypes.Update(_gamePartTypeMapper.Map(vm.GamePartType)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: GamePartType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePartType = await _bll.GamePartTypes.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gamePartType == null)
            {
                return NotFound();
            }

            return View(_gamePartTypeMapper.Map(gamePartType));
        }

        // POST: GamePartType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GamePartTypes.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
