using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Api controller for TeamPerson
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TeamPersonController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.TeamPersonMapper _teamPersonMapper;
        
        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of teamPerson
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>

        public TeamPersonController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _teamPersonMapper = new TeamPersonMapper(mapper);
        }

        // GET: api/TeamPerson
        /// <summary>
        /// Get all TeamPerson entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all TeamPerson entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.TeamPerson?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.TeamPerson>>> GetTeamPersons()
        {
            return Ok((await _bll.TeamPersons.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _teamPersonMapper.Map(x)));
        }

        // GET: api/TeamPerson/5
        /// <summary>
        /// Get specific TeamPerson which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">TeamPerson unique Id</param>
        /// <returns>TeamPerson entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.TeamPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.TeamPerson>> GetTeam_Person(Guid id)
        {
            var teamPerson = await _bll.TeamPersons.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (teamPerson == null)
            {
                return NotFound();
            }

            return _teamPersonMapper.Map(teamPerson)!;
        }

        // PUT: api/TeamPerson/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update TeamPerson entity
        /// </summary>
        /// <param name="id"> TeamPerson to be changed Id </param>
        /// <param name="teamPerson"> TeamPerson entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.TeamPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutTeam_Person(Guid id, PublicApi.DTO.v1.TeamPerson teamPerson)
        {
            if (id != teamPerson.Id)
            {
                return BadRequest();
            }

            if (!await _bll.TeamPersons.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.TeamPersons.Update(_teamPersonMapper.Map(teamPerson)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/TeamPerson
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 TeamPerson entity into Db
        /// </summary>
        /// <param name="teamPerson">PublicApiVersion1.0 TeamPerson entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.TeamPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.TeamPerson>> PostTeam_Person(PublicApi.DTO.v1.TeamPerson teamPerson)
        {
            var bllEntity = _teamPersonMapper.Map(teamPerson)!;
            _bll.TeamPersons.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.TeamPersons.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _teamPersonMapper.Map(updatedEntity);

            return CreatedAtAction("GetTeam_Person", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/TeamPerson/5
        /// <summary>
        /// Delete TeamPerson entity given it's Id
        /// </summary>
        /// <param name="id"> TeamPerson's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteTeam_Person(Guid id)
        {
            if (!await _bll.TeamPersons.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.TeamPersons.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
