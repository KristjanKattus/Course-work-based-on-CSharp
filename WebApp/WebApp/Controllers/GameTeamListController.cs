using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using Resources.PublicApi.DTO.v1;
using WebApp.ViewModels.GameTeamList;
using GameTeamList = PublicApi.DTO.v1.GameTeamList;
using GameTeamListMapper = PublicApi.DTO.v1.Mappers.GameTeamListMapper;
using Role = Domain.App.Role;
using RoleMapper = PublicApi.DTO.v1.Mappers.RoleMapper;
using TeamPersonMapper = PublicApi.DTO.v1.Mappers.TeamPersonMapper;

namespace WebApp.Controllers
{
    [Authorize]
    public class GameTeamListController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;
        private readonly PublicApi.DTO.v1.Mappers.GameTeamListMapper _gameTeamListMapper;

        public GameTeamListController(IMapper mapper, IAppBLL bll)
        {
            _mapper = mapper;
            _bll = bll;
            _gameTeamListMapper = new GameTeamListMapper(mapper);
        }

        // GET: GameTeamList
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View((await _bll.GameTeamLists.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gameTeamListMapper.Map(x)).ToList());
        }

        // GET: GameTeamList/Details/5
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTeamList = await _bll.GameTeamLists.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameTeamList == null)
            {
                return NotFound();
            }

            return View(_gameTeamListMapper.Map(gameTeamList));
        }

        // GET: GameTeamList/Create
        public async Task<IActionResult> Create(Guid gameTeamId)
        {
            var roleMapper = new RoleMapper(_mapper);

            var vm = new GameTeamListCreateViewModel
            {
                GameTeamId = gameTeamId,
                RoleSelectList = new SelectList(((await _bll.Roles.GetAllAsync()).Where(x => x.Name == "Physio")).Select(x => roleMapper.Map(x))
                    , nameof(Role.Id), nameof(Role.Name))
            };

            return View(vm);

        }

        // POST: GameTeamList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameTeamListCreateViewModel vm)
        {

            if (ModelState.IsValid)
            {
                vm.Person.AppUserId = User.GetUserId()!.Value;
                var GTL = new GameTeamList()
                {
                    GameTeamId = vm.GameTeamId,
                    Person = vm.Person,
                    RoleId = vm.RoleId,
                    Staff = true,
                };
                _bll.GameTeamLists.Add(_gameTeamListMapper.Map(GTL)!);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Details", "Game", new{id = (await _bll.GameTeams.FirstOrDefaultAsync(vm.GameTeamId))!.GameId});
            }
            var roleMapper = new RoleMapper(_mapper);
            vm.RoleSelectList = new SelectList((await _bll.Roles.GetAllAsync()).Select(x => roleMapper.Map(x))
                , nameof(Role.Id), nameof(Role.Name));

            return View(vm);
        }

        // GET: GameTeamList/Edit/5
        public async Task<IActionResult> Edit(Guid gameTeamId)
        {
            var gameTeam = (await _bll.GameTeams.FirstOrDefaultAsync(gameTeamId))!;
            var teamId = gameTeam.TeamId;

            var clientTeam = await _bll.Teams.GetClientTeamAsync(teamId, _mapper);
            if (clientTeam == null)
            {
                return NotFound();
            }

            
            var vm = new GameTeamListCreateEditViewModel
            {
                GameTeamId = gameTeamId,
                GameTeamName = gameTeam.TeamName,
                PlayerList = new List<AddGameMember>(),
                StaffList = new List<AddGameMember>()
            };
            var teamPersonMapper = new TeamPersonMapper(_mapper);
            foreach (var player in clientTeam.PlayerList!)
            {
                vm.PlayerList.Add(new AddGameMember
                {
                    Person = teamPersonMapper.Map(player)!,
                    PersonId = player.Id
                });
            }
            foreach (var player in clientTeam.StaffList!)
            {
                vm.StaffList.Add(new AddGameMember
                {
                    Person = teamPersonMapper.Map(player)!,
                    PersonId = player.Id
                });
            }
            
            
            return View(vm);
        }

        // POST: GameTeamList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GameTeamListCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var player in vm.PlayerList!.Where(x => x.PartOfGame))
                {
                    _bll.GameTeamLists.AddTeamPersonToList(vm.GameTeamId, player.PersonId, player.InStartingLineup);
                }

                if (vm.StaffList != null)
                {
                    foreach (var staff in vm.StaffList!.Where(x => x.PartOfGame))
                    {
                        _bll.GameTeamLists.AddTeamPersonToList(vm.GameTeamId, staff.PersonId);
                    }
                }
                
                return RedirectToAction("Details", "Game", new{id = (await _bll.GameTeams.FirstOrDefaultAsync(vm.GameTeamId))!.GameId});
            }
            return View(vm);
        } 

        // GET: GameTeamList/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTeamList = await _bll.GameTeamLists.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (gameTeamList == null)
            {
                return NotFound();
            }

            return View(_gameTeamListMapper.Map(gameTeamList));
        }

        // POST: GameTeamList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GameTeamLists.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
