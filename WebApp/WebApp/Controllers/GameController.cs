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
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels.Club;
using WebApp.ViewModels.Game;
using Game = DAL.App.DTO.Game;
using GameEvent = BLL.App.DTO.GameEvent;
using League = BLL.App.DTO.League;
using Stadium = BLL.App.DTO.Stadium;
using Team = BLL.App.DTO.Team;

namespace WebApp.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create(Guid leagueId)
        {
            var leagueTeams = (await _bll.LeagueTeams.GetAllWithLeagueIdAsync(leagueId)).ToList();
            var teamList = new List<BLL.App.DTO.Team>();
            foreach (var leagueTeam in leagueTeams)
            {
                teamList.Add((await _bll.Teams.FirstOrDefaultAsync(leagueTeam.TeamId)!)!);
            }

            var vm = new GameCreateEditViewModel
            {
                LeagueId = leagueId,
                StadiumSelectList = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value),
                    nameof(Stadium.Id), nameof(Stadium.Name)),
                HomeTeamSelectList = new SelectList(teamList
                    , nameof(Team.Id), nameof(Team.Name)),
                AwayTeamSelectList = new SelectList(teamList
                    , nameof(Team.Id), nameof(Team.Name))
            };
            
            Console.Write(vm.HomeTeamSelectList.Items);
            Console.Write(vm.AwayTeamSelectList.Items);
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
                vm.Game.LeagueId = vm.LeagueId;
                var game = _bll.Games.Add(_gameMapper.Map(vm.Game)!);

                var homeTeam = new BLL.App.DTO.GameTeam
                {
                    GameId = game.Id,
                    TeamId = (Guid) vm.HomeTeamId!,
                    Hometeam = true
                };
                var awayTeam = new BLL.App.DTO.GameTeam
                {
                    GameId = game.Id,
                    TeamId = (Guid) vm.AwayTeamId!,
                    Hometeam = false
                };
                _bll.GameTeams.Add(homeTeam);
                _bll.GameTeams.Add(awayTeam);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var leagueTeams = (await _bll.LeagueTeams.GetAllWithLeagueIdAsync(vm.Game.LeagueId)).ToList();
            var teamList = new List<BLL.App.DTO.Team>();
            foreach (var leagueTeam in leagueTeams)
            {
                teamList.Add((await _bll.Teams.FirstOrDefaultAsync(leagueTeam.TeamId)!)!);
            }

            vm.StadiumSelectList = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value)
                , nameof(Stadium.Id), nameof(Stadium.Name));
            vm.HomeTeamSelectList = new SelectList(teamList, nameof(Team.Name), nameof(Team.Name));
            vm.AwayTeamSelectList = new SelectList(teamList, nameof(Team.Name), nameof(Team.Name));
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
            
            var leagueTeams = (await _bll.LeagueTeams.GetAllWithLeagueIdAsync(game.LeagueId)).ToList();
            var teamList = new List<BLL.App.DTO.Team>();
            foreach (var leagueTeam in leagueTeams)
            {
                teamList.Add((await _bll.Teams.FirstOrDefaultAsync(leagueTeam.TeamId)!)!);
            }
            
            var vm = new GameCreateEditViewModel
            {
                Game = _gameMapper.Map(game)!,
                StadiumSelectList = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value),
                    nameof(Stadium.Id), nameof(Stadium.Name)),
                HomeTeamSelectList = new SelectList(teamList
                    , nameof(Team.Id), nameof(Team.Name)),
                AwayTeamSelectList = new SelectList(teamList
                    , nameof(Team.Id), nameof(Team.Name))
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
            
            var leagueTeams = (await _bll.LeagueTeams.GetAllWithLeagueIdAsync(vm.Game.LeagueId)).ToList();
            var teamList = new List<BLL.App.DTO.Team>();
            foreach (var leagueTeam in leagueTeams)
            {
                teamList.Add((await _bll.Teams.FirstOrDefaultAsync(leagueTeam.TeamId)!)!);
            }

            vm.StadiumSelectList = new SelectList(await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value)
                , nameof(Stadium.Id), nameof(Stadium.Name));
            vm.HomeTeamSelectList = new SelectList(teamList, nameof(Team.Name), nameof(Team.Name));
            vm.AwayTeamSelectList = new SelectList(teamList, nameof(Team.Name), nameof(Team.Name));
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
            await _bll.GameTeams.RemoveGamesWithGameIdAsync(id);

            var game = await _bll.Games.FirstOrDefaultAsync(id);

            _bll.Games.Remove(game, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
