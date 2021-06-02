using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;
using GameEvent = PublicApi.DTO.v1.GameEvent;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Get Game Events
    /// </summary>
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class GameEventController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GameEventMapper _gameEventMapper;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">AppDbContext</param>
        /// <param name="mapper">Imapper</param>
        /// <param name="bll">Business logic</param>
        public GameEventController(AppDbContext context, IMapper mapper, IAppBLL bll)
        {
            _mapper = mapper;
            _bll = bll;
            _gameEventMapper = new GameEventMapper(mapper);
        }

        // GET: api/GameEvent
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("gameId={gameId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.GameEvent?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IEnumerable<GameEvent?>> GetGameEvents(Guid gameId)
        {
            return (await _bll.GameEvents.GetWithGameIdAsync(gameId)).Select(x => _gameEventMapper.Map(x));
        }

        // GET: api/GameEvent/5
        /// <summary>
        /// Get first GameEvent which matches Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PublicApi.DTO.v1.GameEvent?> GetGameEvent(Guid id)
        {
            var gameEvent = await _bll.GameEvents.FirstOrDefaultAsync(id);

            return _gameEventMapper.Map(gameEvent);
        }

        // PUT: api/GameEvent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update GameEvent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gameEvent"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGameEvent(Guid id, PublicApi.DTO.v1.GameEvent gameEvent)
        {
            if (id != gameEvent.Id)
            {
                return BadRequest();
            }

            if (!await _bll.Games.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.GameEvents.Update(_gameEventMapper.Map(gameEvent)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/GameEvent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add GameEvent to db
        /// </summary>
        /// <param name="gameEvent"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameEvent), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.GameEvent>> PostGameEvent(PublicApi.DTO.v1.GameEvent gameEvent)
        {
           
            
            var returnEntity = _bll.GameEvents.Add(_gameEventMapper.Map(gameEvent)!);
            await _bll.SaveChangesAsync();
            await _bll.GameTeams.UpdateEntity(returnEntity.Id);

            return CreatedAtAction("GetGameEvent", new { id = returnEntity.Id }, returnEntity);
        }

        // DELETE: api/GameEvent/5
        /// <summary>
        /// Delete GameEvent with given entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGameEvent(Guid id)
        {
            

            await _bll.GameEvents.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
