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
    /// Api controller for clubs
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClubController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.ClubMapper _clubMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of clubMapper.
        /// </summary>
        /// <param name="bll"> Business layer</param>
        /// <param name="mapper"> Automapper </param>
        public ClubController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _clubMapper = new ClubMapper(mapper);
        }

        // GET: api/Club
        /// <summary>
        /// Get all Club entities in PublicApiVersion1.0.
        /// Allowed for all authorized user level "admin".
        /// </summary>
        /// <returns>PublicApiVersion1.0 all Club entities</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Club?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Club>>> GetClubs()
        {
            return Ok((await _bll.Clubs.GetAllAsync(User.GetUserId()!.Value)).Select(x => _clubMapper.Map(x)));
        }

        // GET: api/Club/5
        /// <summary>
        /// Get specific club which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">Club's unique ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Club), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.Club>> GetClub(Guid id)
        {
            var club = await _bll.Clubs.FirstOrDefaultAsync(id!,User.GetUserId()!.Value);

            if (club == null)
            {
                return NotFound();
            }

            return _clubMapper.Map(club)!;
        }

        // PUT: api/Club/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Club entitty
        /// </summary>
        /// <param name="id">Club to be changed ID</param>
        /// <param name="club">CLub entity to be updated</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Club), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutClub(Guid id, PublicApi.DTO.v1.Club club)
        {
            if (id != club.Id)
            {
                return BadRequest();
            }

            if (await _bll.Clubs.FirstOrDefaultAsync(id, User.GetUserId()!.Value) == null)
            {
                return BadRequest();
            }

            _bll.Clubs.Update(_clubMapper.Map(club)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Club
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add created entity into db
        /// </summary>
        /// <param name="club"> PublicApiVersion1.0 Club entity to be added </param>
        /// <returns> Created Action with details </returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Club), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Club), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.Club>> PostClub(PublicApi.DTO.v1.Club club)
        {
            var bllEntity = _clubMapper.Map(club)!;
            _bll.Clubs.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Clubs.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _clubMapper.Map(updatedEntity);

            return CreatedAtAction("GetClub", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/Club/5
        /// <summary>
        /// Delete Club entity given entities Id.
        /// </summary>
        /// <param name="id">Entity specific Id</param>
        /// <returns>If entity does not exist in db, return NotFound</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteClub(Guid id)
        {
            if (await _bll.Clubs.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.Clubs.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
