using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Api controller for StadiumArea
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StadiumAreaController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.StadiumAreaMapper _stadiumAreaMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of stadiumAreaMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public StadiumAreaController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _stadiumAreaMapper = new StadiumAreaMapper(mapper);
        }

        // GET: api/StadiumArea
        /// <summary>
        /// Get all StadiumArea entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all StadiumArea entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.StadiumArea?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.StadiumArea>>> GetStadiumAreas()
        {
            return Ok((await _bll.StadiumAreas.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _stadiumAreaMapper.Map(x)));
        }

        // GET: api/StadiumArea/5
        /// <summary>
        /// Get specific StadiumArea which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">StadiumArea unique Id</param>
        /// <returns>StadiumArea entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.StadiumArea), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.StadiumArea>> GetStadium_Area(Guid id)
        {
            var stadiumArea = await _bll.StadiumAreas.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (stadiumArea == null)
            {
                return NotFound();
            }

            return _stadiumAreaMapper.Map(stadiumArea)!;
        }

        // PUT: api/StadiumArea/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update StadiumArea entity
        /// </summary>
        /// <param name="id"> StadiumArea to be changed Id </param>
        /// <param name="stadiumArea"> StadiumArea entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.StadiumArea), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutStadium_Area(Guid id, PublicApi.DTO.v1.StadiumArea stadiumArea)
        {
            if (id != stadiumArea.Id)
            {
                return BadRequest();
            }
            
            if (!await _bll.StadiumAreas.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.StadiumAreas.Update(_stadiumAreaMapper.Map(stadiumArea)!);
            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/StadiumArea
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 StadiumArea entity into Db
        /// </summary>
        /// <param name="stadiumArea">PublicApiVersion1.0 StadiumArea entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.StadiumArea), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.StadiumArea>> PostStadium_Area(PublicApi.DTO.v1.StadiumArea stadiumArea)
        {
            var bllEntity = _stadiumAreaMapper.Map(stadiumArea)!;
            _bll.StadiumAreas.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.StadiumAreas.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _stadiumAreaMapper.Map(updatedEntity);

            return CreatedAtAction("GetStadium_Area", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/StadiumArea/5
        /// <summary>
        /// Delete StadiumArea entity given it's Id
        /// </summary>
        /// <param name="id"> StadiumArea's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteStadium_Area(Guid id)
        {
            if (!await _bll.StadiumAreas.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.StadiumAreas.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
