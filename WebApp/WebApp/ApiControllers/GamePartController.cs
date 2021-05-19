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
    /// Api controller for GameParts
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamePartController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GamePartMapper _gamePartMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of GamePartMapper
        /// </summary>
        /// <param name="mapper"> Automapper </param>
        /// <param name="bll"> Business layer </param>
        public GamePartController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gamePartMapper = new GamePartMapper(mapper);
        }

        // GET: api/GamePart
        /// <summary>
        /// Get all GamePart entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns> PublicApiVersion1.0 all GamePart entities </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.GamePart?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.GamePart>>> GetGameParts()
        {
            return Ok((await _bll.GameParts.GetAllAsync(User.GetUserId()!.Value))
                 .Select(x => _gamePartMapper.Map(x)));
        }

        // GET: api/GamePart/5
        /// <summary>
        /// Get specific GamePart which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id"> GamePart unique Id </param>
        /// <returns> GamePart entity of PublicApi.DTO.v1 </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePart), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.GamePart>> GetGame_Part(Guid id)
        {
            var gamePart = await _bll.GameParts.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (gamePart == null)
            {
                return NotFound();
            }

            return _gamePartMapper.Map(gamePart)!;
        }

        // PUT: api/GamePart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update GamePart entity
        /// </summary>
        /// <param name="id"> GamePart to be changed Id </param>
        /// <param name="gamePart"> GamePart entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePart), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        
        public async Task<IActionResult> PutGame_Part(Guid id, PublicApi.DTO.v1.GamePart gamePart)
        {
            if (id != gamePart.Id)
            {
                return BadRequest();
            }

            if (!await _bll.GameParts.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.GameParts.Update(_gamePartMapper.Map(gamePart)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/GamePart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 GamePart entity into Db
        /// </summary>
        /// <param name="gamePart"> PublicApiVersion1.0 GamePart entity to be added  </param>
        /// <returns> Created Action with details of added entity </returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePart), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.GamePart>> PostGame_Part(PublicApi.DTO.v1.GamePart gamePart)
        {
            var bllEntity = _gamePartMapper.Map(gamePart)!;
            _bll.GameParts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.GameParts.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _gamePartMapper.Map(updatedEntity);

            return CreatedAtAction("GetGame_Part", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/GamePart/5
        /// <summary>
        /// Delete GamePart entity given it's Id
        /// </summary>
        /// <param name="id"> GamePart's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGame_Part(Guid id)
        {
            
            if (!await _bll.GameParts.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.GameParts.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
