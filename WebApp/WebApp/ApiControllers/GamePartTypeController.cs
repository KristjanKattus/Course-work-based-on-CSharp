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
    /// Api controller for GamePartTypes
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamePartTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GamePartTypeMapper _gamePartTypeMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of GamePartTypeMapper
        /// </summary>
        /// <param name="mapper"> Automapper </param>
        /// <param name="bll"> Business layer </param>
        public GamePartTypeController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gamePartTypeMapper = new GamePartTypeMapper(mapper);
        }

        // GET: api/GamePartType
        /// <summary>
        /// Get all GamePartType entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns> PublicApiVersion1.0 all GamePartType entities </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.GamePartType?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.GamePartType>>> GetTypes()
        {
            return Ok((await _bll.GamePartTypes.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gamePartTypeMapper.Map(x)));
        }

        // GET: api/GamePartType/5
        /// <summary>
        /// Get specific GamePartType which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id"> GamePartType unique Id </param>
        /// <returns> GamePartType entity of PublicApi.DTO.v1 </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePartType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.GamePartType>> GetGame_Part_Type(Guid id)
        {
            var gamePartType = await _bll.GamePartTypes.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (gamePartType == null)
            {
                return NotFound();
            }

            return _gamePartTypeMapper.Map(gamePartType)!;
        }

        // PUT: api/GamePartType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update GamePartType entity
        /// </summary>
        /// <param name="id"> GamePartType to be changed Id </param>
        /// <param name="gamePartType"> GamePartType entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePartType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGame_Part_Type(Guid id, PublicApi.DTO.v1.GamePartType gamePartType)
        {
            if (id != gamePartType.Id)
            {
                return BadRequest();
            }

            if (!await _bll.GamePartTypes.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.GamePartTypes.Update(_gamePartTypeMapper.Map(gamePartType)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/GamePartType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 GamePartType entity into Db
        /// </summary>
        /// <param name="gamePartType"> PublicApiVersion1.0 GamePartType entity to be added </param>
        /// <returns> Created Action with details of added entity </returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePartType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Game_Part_Type>> PostGame_Part_Type(PublicApi.DTO.v1.GamePartType gamePartType)
        {
            var bllEntity = _gamePartTypeMapper.Map(gamePartType)!;
            _bll.GamePartTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.GamePartTypes.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _gamePartTypeMapper.Map(updatedEntity);

            return CreatedAtAction("GetGame_Part_Type", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/GamePartType/5
        /// <summary>
        /// Delete GamePartType entity given it's Id
        /// </summary>
        /// <param name="id"> GamePartType's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGame_Part_Type(Guid id)
        {
            if (!await _bll.GamePartTypes.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.GamePartTypes.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
