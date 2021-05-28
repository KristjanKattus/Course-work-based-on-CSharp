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
using WebApp.ViewModels.Role;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.RoleMapper _roleMapper;

        public RoleController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _roleMapper = new RoleMapper(mapper);
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Roles.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _roleMapper.Map(x)).ToList());
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _bll.Roles.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View(_roleMapper.Map(role));
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            var vm = new RoleCreateEditViewModel();
            return View(vm);
        }

        // POST: Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Roles.Add(_roleMapper.Map(vm.Role)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _bll.Roles.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (role == null)
            {
                return NotFound();
            }
            var vm = new RoleCreateEditViewModel{Role = _roleMapper.Map(role)!};
            return View(vm);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RoleCreateEditViewModel vm)
        {
            if (id != vm.Role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Roles.Update(_roleMapper.Map(vm.Role)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _bll.Roles.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View(_roleMapper.Map(role));
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Roles.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
