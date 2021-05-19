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
    /// Api controller for GamePersonnel
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamePersonnelController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GamePersonnelMapper _gamePersonnelMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of GamePersonnelMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public GamePersonnelController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gamePersonnelMapper = new GamePersonnelMapper(mapper);
        }

        // GET: api/GamePersonnel
        /// <summary>
        /// Get all GamePersonnel entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all GamePersonnel entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.GamePersonnel?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.GamePersonnel>>> GetGamePersonnels()
        {
            return Ok((await _bll.GamePersonnel.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gamePersonnelMapper.Map(x)));
        }

        // GET: api/GamePersonnel/5
        /// <summary>
        /// Get specific GamePersonnel which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">GamePersonnel unique Id</param>
        /// <returns>GamePersonnel entity of PublicApi.DTO.v1 </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePersonnel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.GamePersonnel>> GetGame_Personnel(Guid id)
        {
            var gamePersonnel = await _bll.GamePersonnel.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (gamePersonnel == null)
            {
                return NotFound();
            }

            return _gamePersonnelMapper.Map(gamePersonnel)!;
        }

        // PUT: api/GamePersonnel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update GamePersonnel entity
        /// </summary>
        /// <param name="id"> GamePartType to be changed Id </param>
        /// <param name="gamePersonnel"> GamePersonnel entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePersonnel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGame_Personnel(Guid id, PublicApi.DTO.v1.GamePersonnel gamePersonnel)
        {
            if (id != gamePersonnel.Id)
            {
                return BadRequest();
            }

            if (!await _bll.GamePersonnel.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.GamePersonnel.Update(_gamePersonnelMapper.Map(gamePersonnel)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/GamePersonnel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 GamePersonnel entity into Db
        /// </summary>
        /// <param name="gamePersonnel">PublicApiVersion1.0 GamePersonnel entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GamePersonnel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.GamePersonnel>> PostGame_Personnel(PublicApi.DTO.v1.GamePersonnel gamePersonnel)
        {
            var bllEntity = _gamePersonnelMapper.Map(gamePersonnel)!;
            _bll.GamePersonnel.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.GamePersonnel.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _gamePersonnelMapper.Map(updatedEntity);

            return CreatedAtAction("GetGame_Personnel", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/GamePersonnel/5
        /// <summary>
        /// Delete GamePersonnel entity given it's Id
        /// </summary>
        /// <param name="id">GamePersonnel's Id to be deleted</param>
        /// <returns>NotFound if entity does not exist in Db</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGame_Personnel(Guid id)
        {
            if (!await _bll.GamePersonnel.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.GamePersonnel.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
