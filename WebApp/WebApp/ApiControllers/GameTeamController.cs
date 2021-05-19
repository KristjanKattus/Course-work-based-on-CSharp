using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Api controller for GameTeam
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameTeamController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GameTeamMapper _gameTeamMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of GameTeamMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public GameTeamController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gameTeamMapper = new GameTeamMapper(mapper);
        }

        // GET: api/GameTeam
        /// <summary>
        /// Get all GameTeam entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all GameTeam entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.GameTeam?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.GameTeam>>> GetGameTeams()
        {
            return Ok((await _bll.GameTeams.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gameTeamMapper.Map(x)));
        }

        // GET: api/GameTeam/5
        /// <summary>
        /// Get specific GameTeam which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">GameTeam unique Id</param>
        /// <returns>GameTeam entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.GameTeam>> GetGame_Team(Guid id)
        {
            var gameTeam = await _bll.GameTeams.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (gameTeam == null)
            {
                return NotFound();
            }

            return _gameTeamMapper.Map(gameTeam)!;
        }

        // PUT: api/GameTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update GameTeam entity
        /// </summary>
        /// <param name="id"> GameTeam to be changed Id </param>
        /// <param name="gameTeam"> GameTeam entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGame_Team(Guid id, PublicApi.DTO.v1.GameTeam gameTeam)
        {
            if (id != gameTeam.Id)
            {
                return BadRequest();
            }

            if (!await _bll.GameTeams.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.GameTeams.Update(_gameTeamMapper.Map(gameTeam)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/GameTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 GameTeam entity into Db
        /// </summary>
        /// <param name="gameTeam">PublicApiVersion1.0 GameTeam entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.GameTeam>> PostGame_Team(PublicApi.DTO.v1.GameTeam gameTeam)
        {
            
            var bllEntity = _gameTeamMapper.Map(gameTeam)!;
            _bll.GameTeams.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.GameTeams.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _gameTeamMapper.Map(updatedEntity);

            return CreatedAtAction("GetGame_Team", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/GameTeam/5
        /// <summary>
        /// Delete GameTeam entity given it's Id
        /// </summary>
        /// <param name="id"> GameTeam's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGame_Team(Guid id)
        {
            if (!await _bll.GameTeams.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.GameTeams.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
