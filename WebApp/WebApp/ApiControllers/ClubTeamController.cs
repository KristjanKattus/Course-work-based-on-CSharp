using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Api controller for ClubTeams
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClubTeamController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.ClubTeamMapper _clubTeamMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of clubTeamMapper.
        /// </summary>
        /// <param name="mapper"> Automapper </param>
        /// <param name="bll"> Business layer</param>
        public ClubTeamController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _clubTeamMapper = new ClubTeamMapper(mapper);
        }

        // GET: api/ClubTeam
        /// <summary>
        /// Get all ClubTeam entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all ClubTeam entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.ClubTeam?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.ClubTeam>>> GetClubTeams()
        {
            return Ok((await _bll.ClubTeams.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _clubTeamMapper.Map(x)));
        }

        // GET: api/ClubTeam/5
        /// <summary>
        /// Get specific clubTeam which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id"> ClubTeams unique Id</param>
        /// <returns> ClubTeam entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.ClubTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.ClubTeam>> GetClub_Team(Guid id)
        {
            var clubTeam = await _bll.ClubTeams.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (clubTeam == null)
            {
                return NotFound();
            }

            return _clubTeamMapper.Map(clubTeam)!;
        }

        // PUT: api/ClubTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update clubTeam entity
        /// </summary>
        /// <param name="id"> Club to be changed Id</param>
        /// <param name="clubTeam"> ClubTeam entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.ClubTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutClub_Team(Guid id, PublicApi.DTO.v1.ClubTeam clubTeam)
        {
            if (id != clubTeam.Id)
            {
                return BadRequest();
            }

            if (await _bll.ClubTeams.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.ClubTeams.Update(_clubTeamMapper.Map(clubTeam)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ClubTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 ClubTeam entity into Db
        /// </summary>
        /// <param name="clubTeam"> PublicApiVersion1.0 ClubTeam entity to be added </param>
        /// <returns> Created Action with details of added entity </returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.ClubTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.ClubTeam), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.ClubTeam>> PostClub_Team(PublicApi.DTO.v1.ClubTeam clubTeam)
        {
            var bllEntity = _clubTeamMapper.Map(clubTeam)!;
            _bll.ClubTeams.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.ClubTeams.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _clubTeamMapper.Map(updatedEntity);

            return CreatedAtAction("GetClub_Team", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/ClubTeam/5
        /// <summary>
        /// Delete ClubTeam entity given it's Id
        /// </summary>
        /// <param name="id"> CLubTeam's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteClub_Team(Guid id)
        {
            if (!await _bll.ClubTeams.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.ClubTeams.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
