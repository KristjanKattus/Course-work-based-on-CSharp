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
using DAL.App.EF.Mappers;
using Domain.App;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels;
using StadiumMapper = PublicApi.DTO.v1.Mappers.StadiumMapper;

namespace WebApp.Controllers
{
    public class LeagueTableController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;
        private readonly PublicApi.DTO.v1.Mappers.LeagueTableMapper _leagueTableMapper;
        private readonly PublicApi.DTO.v1.Mappers.LeagueGameMapper _leagueGameMapper;

        public LeagueTableController(IMapper mapper, IAppBLL bll)
        {
            _mapper = mapper;
            _bll = bll;
            _leagueGameMapper = new LeagueGameMapper(mapper);
            _leagueTableMapper = new LeagueTableMapper(mapper);
        }

        // GET: LeagueTable
        public async Task<IActionResult> Index(Guid id)
        {
            var stadiumMapper = new StadiumMapper(_mapper);
            var leagueGames = (await _bll.Games.GetAllLeagueGameAsync(id, _mapper))
                .Select(x => _leagueGameMapper.Map(x)).ToList()!;
            foreach (var game in leagueGames)
            {
                game!.Game!.Stadium = stadiumMapper.Map(await _bll.Stadiums.FirstOrDefaultAsync(game.Game.StadiumId));
            }
            var vm = new LeagueTableIndexViewModel();
            vm.LeagueName = (await _bll.Leagues.FirstOrDefaultAsync(id))!.Name;
            vm.LeagueTableTeams =
                (await _bll.LeagueTeams.GetAllLeagueTeamsDataAsync(id)).Select(x => _leagueTableMapper.Map(x))
                .ToList()!;
            vm.LeagueGames = leagueGames!;
            return View(vm);
        }
    }
}