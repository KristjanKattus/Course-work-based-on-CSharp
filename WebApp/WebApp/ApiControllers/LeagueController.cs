using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
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
    /// Api controller for League
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LeagueController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.LeagueMapper _leagueMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of LeagueMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public LeagueController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _leagueMapper = new LeagueMapper(mapper);
        }

        // GET: api/League
        /// <summary>
        /// Get all League entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all League entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.League?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.League>>> GetLeagues()
        {
            return Ok((await _bll.Leagues.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _leagueMapper.Map(x)));
        }

        // GET: api/League/5
        /// <summary>
        /// Get specific League which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">League unique Id</param>
        /// <returns>League entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.League), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.League>> GetLeague(Guid id)
        {
            var league = await _bll.Leagues.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (league == null)
            {
                return NotFound();
            }

            return _leagueMapper.Map(league)!;
        }

        // PUT: api/League/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update League entity
        /// </summary>
        /// <param name="id"> League to be changed Id </param>
        /// <param name="league"> League entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.League), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutLeague(Guid id, PublicApi.DTO.v1.League league)
        {
            if (id != league.Id)
            {
                return BadRequest();
            }
            
            if (!await _bll.Leagues.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.Leagues.Update(_leagueMapper.Map(league)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/League
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 League entity into Db
        /// </summary>
        /// <param name="league">PublicApiVersion1.0 League entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.League), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.League>> PostLeague(PublicApi.DTO.v1.League league)
        {
            var bllEntity = _leagueMapper.Map(league)!;
            _bll.Leagues.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Leagues.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _leagueMapper.Map(updatedEntity);

            return CreatedAtAction("GetLeague", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/League/5
        /// <summary>
        /// Delete League entity given it's Id
        /// </summary>
        /// <param name="id"> League's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteLeague(Guid id)
        {
            if (!await _bll.Leagues.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.Leagues.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
