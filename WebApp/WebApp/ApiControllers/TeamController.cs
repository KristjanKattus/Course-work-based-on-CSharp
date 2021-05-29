using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Api controller for Team
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.TeamMapper _teamMapper;
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of teamMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>

        public TeamController(IMapper mapper, IAppBLL bll)
        {
            _mapper = mapper;
            _bll = bll;
            _teamMapper = new TeamMapper(mapper);
        }

        // GET: api/Team
        /// <summary>
        /// Get all Team entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all Team entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Team?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Team>>> GetTeams()
        {
            return Ok((await _bll.Teams.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _teamMapper.Map(x)));
        }

        // GET: api/Team/5
        /// <summary>
        /// Get specific Team which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">Team unique Id</param>
        /// <returns>Team entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        
        [ProducesResponseType(typeof(PublicApi.DTO.v1.ClientTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.ClientTeam>> GetTeam(Guid id)
        {
            var team = await _bll.Teams.GetClientTeamAsync(id, _mapper);
            if (team == null)
            {
                return NotFound();
            }

            var clientTeamMapper = new ClientTeamMapper(_mapper);

            return clientTeamMapper.Map(team)!;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Team entity
        /// </summary>
        /// <param name="id"> Team to be changed Id </param>
        /// <param name="team"> Team entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Team), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutTeam(Guid id, PublicApi.DTO.v1.Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            } 
            
            if (!await _bll.Teams.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.Teams.Update(_teamMapper.Map(team)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Team
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 Team entity into Db
        /// </summary>
        /// <param name="team">PublicApiVersion1.0 Team entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Team), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.Team>> PostTeam(PublicApi.DTO.v1.Team team)
        {
            var bllEntity = _teamMapper.Map(team)!;
            _bll.Teams.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Teams.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _teamMapper.Map(updatedEntity);

            return CreatedAtAction("GetTeam", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/Team/5
        /// <summary>
        /// Delete Team entity given it's Id
        /// </summary>
        /// <param name="id"> Team's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            if (!await _bll.Teams.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.Teams.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
