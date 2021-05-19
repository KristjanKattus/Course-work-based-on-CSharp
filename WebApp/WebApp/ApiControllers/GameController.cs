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
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Api controller for Games
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GameMapper _gameMapper;
        
        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of GameMapper
        /// </summary>
        /// <param name="mapper"> Automapper </param>
        /// <param name="bll"> Business layer </param>
        public GameController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gameMapper = new GameMapper(mapper);
        }

        // GET: api/Game
        /// <summary>
        /// Get all Game entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns> PublicApiVersion1.0 all Game entities </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Game?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Game>>> GetGames()
        {
            return Ok((await _bll.Games.GetAllAsync(User.GetUserId()!.Value)).Select(x => _gameMapper.Map(x)));
        }

        // GET: api/Game/5
        /// <summary>
        /// Get specific Game which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id"> Game unique Id </param>
        /// <returns>Game entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.Game>> GetGame(Guid id)
        {
            var game = await _bll.Games.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (game == null)
            {
                return NotFound();
            }

            return _gameMapper.Map(game)!;
        }

        // PUT: api/Game/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Game entity
        /// </summary>
        /// <param name="id"> Game to be changed Id </param>
        /// <param name="game"> Game entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGame(Guid id, PublicApi.DTO.v1.Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            if (!await _bll.Games.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.Games.Update(_gameMapper.Map(game)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 Game entity into Db
        /// </summary>
        /// <param name="game"> PublicApiVersion1.0 Game entity to be added </param>
        /// <returns> Created Action with details of added entity </returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Game), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.Game>> PostGame(PublicApi.DTO.v1.Game game)
        {
            var bllEntity = _gameMapper.Map(game)!;
            _bll.Games.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Games.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _gameMapper.Map(updatedEntity);

            return CreatedAtAction("GetGame", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/Game/5
        /// <summary>
        /// Delete Game entity given it's Id
        /// </summary>
        /// <param name="id"> Game's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            if (!await _bll.Games.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.Games.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
