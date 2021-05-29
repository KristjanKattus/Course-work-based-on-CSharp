using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Api controller for LeagueTeam
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class LeagueTeamController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.LeagueTeamMapper _leagueTeamMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of LeagueTeamMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public LeagueTeamController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _leagueTeamMapper = new LeagueTeamMapper(mapper);
        }

        // GET: api/LeagueTeam
        /// <summary>
        /// Get all LeagueTeam entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all LeagueTeam entities</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.LeagueTeam?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.LeagueTeam>>> GetLeagueTeams()
        {
            return Ok((await _bll.LeagueTeams.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _leagueTeamMapper.Map(x)));
        }

        // GET: api/LeageTeam/5
        /// <summary>
        /// Get specific League which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">LeagueTeam unique Id</param>
        /// <returns>LeagueTeam entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.LeagueTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.LeagueTeam>> GetLeagueTeam(Guid id)
        {
            var leagueTeam = await _bll.LeagueTeams.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (leagueTeam == null)
            {
                return NotFound();
            }

            return _leagueTeamMapper.Map(leagueTeam)!;
        }

        // PUT: api/LeageTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update LeagueTeam entity
        /// </summary>
        /// <param name="id"> LeagueTeam to be changed Id </param>
        /// <param name="leagueTeam"> LeagueTeam entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.LeagueTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutLeagueTeam(Guid id, PublicApi.DTO.v1.LeagueTeam leagueTeam)
        {
            if (id != leagueTeam.Id)
            {
                return BadRequest();
            }
            
            if (!await _bll.LeagueTeams.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.LeagueTeams.Update(_leagueTeamMapper.Map(leagueTeam)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/LeageTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 LeagueTeam entity into Db
        /// </summary>
        /// <param name="leagueTeam">PublicApiVersion1.0 LeagueTeam entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.LeagueTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.LeagueTeam>> PostLeagueTeam(PublicApi.DTO.v1.LeagueTeam leagueTeam)
        {
            var bllEntity = _leagueTeamMapper.Map(leagueTeam)!;
            _bll.LeagueTeams.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.LeagueTeams.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _leagueTeamMapper.Map(updatedEntity);
            return CreatedAtAction("GetLeagueTeam", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/LeageTeam/5
        /// <summary>
        /// Delete LeagueTeam entity given it's Id
        /// </summary>
        /// <param name="id"> LeagueTeam's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteLeagueTeam(Guid id)
        {
            if (!await _bll.LeagueTeams.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.LeagueTeams.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
