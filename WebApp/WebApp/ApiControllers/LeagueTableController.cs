﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{

    /// <summary>
    /// Api controller for League
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LeagueTableController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of LeagueMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public LeagueTableController(IMapper mapper, IAppBLL bll)
        {
            _mapper = mapper;
            _bll = bll;
        }

        // GET: api/League
        /// <summary>
        /// GetLeagueTableClient entity in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all LeagueTableClient entities</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.LeagueTableClient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.LeagueTableClient>> GetLeagueTable(Guid id)
        {
            var leagueGameMapper = new LeagueGameMapper(_mapper);
            var leagueTableMapper = new LeagueTableMapper(_mapper);
            var leagueTableClient = new LeagueTableClient
            {
                LeagueName = (await _bll.Leagues.FirstOrDefaultAsync(id))!.Name,
                LeagueTableTeams =
                    (await _bll.LeagueTeams.GetAllLeagueTeamsDataAsync(id)).Select(x => leagueTableMapper.Map(x))
                    .ToList()!,
                LeagueGames = (await _bll.Games.GetAllLeagueGameAsync(id, _mapper))
                    .Select(x => leagueGameMapper.Map(x)).ToList()!
            };
            return Ok(leagueTableClient);
        }
    }
}