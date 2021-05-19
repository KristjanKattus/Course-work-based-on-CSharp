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
    /// Api controller for Stadium
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StadiumController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.StadiumMapper _stadiumMapper;
        
        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of stadiumMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public StadiumController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _stadiumMapper = new StadiumMapper(mapper);
        }

        // GET: api/Stadium
        /// <summary>
        /// Get all Stadium entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all Stadium entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Stadium?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Stadium>>> GetStadiums()
        {
            return Ok((await _bll.Stadiums.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _stadiumMapper.Map(x)));
        }

        // GET: api/Stadium/5
        /// <summary>
        /// Get specific Stadium which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">Stadium unique Id</param>
        /// <returns>Stadium entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Stadium), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.Stadium>> GetStadium(Guid id)
        {
            var stadium = await _bll.Stadiums.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (stadium == null)
            {
                return NotFound();
            }

            return _stadiumMapper.Map(stadium)!;
        }

        // PUT: api/Stadium/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Stadium entity
        /// </summary>
        /// <param name="id"> Stadium to be changed Id </param>
        /// <param name="stadium"> Stadium entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Stadium), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutStadium(Guid id, PublicApi.DTO.v1.Stadium stadium)
        {
            if (id != stadium.Id)
            {
                return BadRequest();
            }

            if (!await _bll.Stadiums.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.Stadiums.Update(_stadiumMapper.Map(stadium)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Stadium
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 Stadium entity into Db
        /// </summary>
        /// <param name="stadium">PublicApiVersion1.0 Stadium entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Stadium), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.Stadium>> PostStadium(PublicApi.DTO.v1.Stadium stadium)
        {
            var bllEntity = _stadiumMapper.Map(stadium)!;
            _bll.Stadiums.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Stadiums.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _stadiumMapper.Map(updatedEntity);

            return CreatedAtAction("GetStadium", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/Stadium/5
        /// <summary>
        /// Delete Stadium entity given it's Id
        /// </summary>
        /// <param name="id"> Stadium's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteStadium(Guid id)
        {
            if (!await _bll.Stadiums.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.Stadiums.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
