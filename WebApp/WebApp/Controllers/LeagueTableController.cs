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
using PublicApi.DTO.v1.Mappers;

namespace WebApp.Controllers
{
    public class LeagueTableController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.LeagueTableMapper _leagueTableMapper;

        public LeagueTableController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _leagueTableMapper = new LeagueTableMapper(mapper);
        }

        // GET: LeagueTable
        public async Task<IActionResult> Index(Guid Id)
        {
            return View((await _bll.LeagueTeams.GetAllLeagueTeamsDataAsync(Id))
                .Select(x => _leagueTableMapper.Map(x)));
        }
    }
}