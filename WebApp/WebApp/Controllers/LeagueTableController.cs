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
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using WebApp.ViewModels;

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
            var vm = new LeagueTableIndexViewModel();
            vm.LeagueName = (await _bll.Leagues.FirstOrDefaultAsync(id))!.Name;
            vm.LeagueTableTeams =
                (await _bll.LeagueTeams.GetAllLeagueTeamsDataAsync(id)).Select(x => _leagueTableMapper.Map(x))
                .ToList()!;
            vm.LeagueGames = (await _bll.Games.GetAllLeagueGameAsync(id, _mapper))
                .Select(x => _leagueGameMapper.Map(x)).ToList()!;
            return View(vm);
        }
    }
}