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
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels.Person;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PersonController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.PersonMapper _personMapper;

        public PersonController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _personMapper = new PersonMapper(mapper);
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Persons.GetAllAsync())
                .Select(x => _personMapper.Map(x)).ToList());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(_personMapper.Map(person));
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            var vm = new PersonCreateEditViewModel();
            return View(vm);
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Person.AppUserId = User.GetUserId()!.Value;
                _bll.Persons.Add(_personMapper.Map(vm.Person)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (person == null)
            {
                return NotFound();
            }

            var vm = new PersonCreateEditViewModel {Person = _personMapper.Map(person)!};
            return View(vm);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonCreateEditViewModel vm)
        {
            if (id != vm.Person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Persons.Update(_personMapper.Map(vm.Person)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(_personMapper.Map(person));
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Persons.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
