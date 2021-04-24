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
using WebApp.ViewModels.EventType;

namespace WebApp.Controllers
{
    public class EventTypeController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.EventTypeMapper _eventTypeMapper;

        public EventTypeController(AppDbContext context, IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _eventTypeMapper = new EventTypeMapper(mapper);
        }

        // GET: EventType
        public async Task<IActionResult> Index()
        {
            return View((await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _eventTypeMapper.Map(x)));
        }

        // GET: EventType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventType = await _bll.EventTypes.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (eventType == null)
            {
                return NotFound();
            }

            return View(_eventTypeMapper.Map(eventType));
        }

        // GET: EventType/Create
        public IActionResult Create()
        {
            var vm = new EventTypeCreateEditViewModel();
            return View();
        }

        // POST: EventType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.EventTypes.Add(_eventTypeMapper.Map(vm.EventType)!);
                
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: EventType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventType = await _bll.EventTypes.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (eventType == null)
            {
                return NotFound();
            }

            var vm = new EventTypeCreateEditViewModel();
            vm.EventType = _eventTypeMapper.Map(eventType)!;
            return View(vm);
        }

        // POST: EventType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EventTypeCreateEditViewModel vm)
        {
            if (id != vm.EventType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.EventTypes.Update(_eventTypeMapper.Map(vm.EventType)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: EventType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventType = await _bll.EventTypes
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (eventType == null)
            {
                return NotFound();
            }
            return View(_eventTypeMapper.Map(eventType));
        }

        // POST: EventType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.EventTypes.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
