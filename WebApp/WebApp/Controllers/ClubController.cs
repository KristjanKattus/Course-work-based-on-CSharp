using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF.Mappers;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Club;


namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClubController : Controller
    {
        
        private readonly IAppBLL _bll;
        
        private readonly PublicApi.DTO.v1.Mappers.ClubMapper _clubMapper;

        public ClubController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _clubMapper = new PublicApi.DTO.v1.Mappers.ClubMapper(mapper);
        }

        // GET: Club
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Clubs.GetAllAsync(User.GetUserId()!.Value)).Select(x => _clubMapper.Map(x)).ToList()!);
        }

        // GET: Club/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _bll.Clubs
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (club == null)
            {
                return NotFound();
            }

            return View(_clubMapper.Map(club));
        }

        // GET: Club/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Club/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClubCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Clubs.Add(_clubMapper.Map(vm.Club)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Club/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _bll.Clubs.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (club == null)
            {
                return NotFound();
            }

            var vm = new ClubCreateEditViewModel();
            vm.Club = _clubMapper.Map(club)!;

            return View(vm);
        }

        // POST: Club/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, ClubCreateEditViewModel vm)
        
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _bll.Clubs.Update(_clubMapper.Map(vm.Club)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Club/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _bll.Clubs
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (club == null)
            {
                return NotFound();
            }

            return View(_clubMapper.Map(club));
        }

        // POST: Club/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var club = await _bll.Clubs.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            _bll.Clubs.Remove(club!, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}