using System;

using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using Contracts.BLL.App;

using Microsoft.AspNetCore.Mvc;

using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels.StadiumArea;
using Stadium = BLL.App.DTO.Stadium;
using StadiumArea = DAL.App.DTO.StadiumArea;


namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for StadiumAreas
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class StadiumAreaController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.StadiumAreaMapper _stadiumAreaMapper;
        
        public StadiumAreaController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _stadiumAreaMapper = new StadiumAreaMapper(mapper);
        }

        // GET: Stadium_Area
        public async Task<IActionResult> Index()
        {
            
            return View((await _bll.StadiumAreas.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _stadiumAreaMapper.Map(x)).ToList());
        }

        // GET: Stadium_Area/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _bll.StadiumAreas
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (area == null)
            {
                return NotFound();
            }

            return View(_stadiumAreaMapper.Map(area));
        }

        // GET: Stadium_Area/Create
        public async Task<IActionResult> Create()
        {
            var vm = new StadiumAreaCreateEditViewModel
                {StadiumSelectList = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(Stadium.Id), nameof(Stadium.Name))};
            return View(vm);
        }

        // POST: Stadium_Area/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StadiumAreaCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.StadiumAreas.Add(_stadiumAreaMapper.Map(vm.StadiumArea)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Stadium_Area/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _bll.StadiumAreas.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (area == null)
            {
                return NotFound();
            }
            
            var vm = new StadiumAreaCreateEditViewModel
            {
                StadiumArea = _stadiumAreaMapper.Map(area)!,
                StadiumSelectList = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value)
                    , nameof(Stadium.Id), nameof(Stadium.Name))
            };

            return View(vm);
        }

        // POST: Stadium_Area/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StadiumAreaCreateEditViewModel vm)
        {
            if (id != vm.StadiumArea.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _bll.StadiumAreas.Update(_stadiumAreaMapper.Map(vm.StadiumArea)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.StadiumSelectList = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value)
                , nameof(Stadium.Id), nameof(Stadium.Name));
            
            return View(vm);
            
            
        }

        // GET: Stadium_Area/Delete/5
            public async Task<IActionResult> Delete(Guid? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var area = await _bll.StadiumAreas
                    .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
                if (area == null)
                {
                    return NotFound();
                }

                return View(_stadiumAreaMapper.Map(area));
            }

            // POST: Stadium_Area/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
                
                await _bll.StadiumAreas.RemoveAsync(id, User.GetUserId()!.Value);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }