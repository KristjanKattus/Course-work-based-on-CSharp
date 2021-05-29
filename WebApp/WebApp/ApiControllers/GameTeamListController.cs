using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Api controller for GameTeamList
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameTeamListController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.GameTeamListMapper _gameTeamListMapper;
        

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of GameTeamListMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public GameTeamListController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _gameTeamListMapper = new GameTeamListMapper(mapper);
        }

        // GET: api/GameTeamList
        /// <summary>
        /// Get all GameTeamList entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all GameTeamList entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.GameTeamList?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.GameTeamList>>> GetGameTeamListMembers()
        {
            return Ok((await _bll.GameTeamLists.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _gameTeamListMapper.Map(x)));
        }

        // GET: api/GameTeamList/5
        /// <summary>
        /// Get specific GameTeamList which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">GameTeamList unique Id</param>
        /// <returns>GameTeamList entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameTeamList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.GameTeamList>> GetGameTeamList(Guid id)
        {
            var gameTeamList = await _bll.GameTeamLists.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (gameTeamList == null)
            {
                return NotFound();
            }

            return _gameTeamListMapper.Map(gameTeamList)!;
        }

        // PUT: api/GameTeamList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update GameTeamList entity
        /// </summary>
        /// <param name="id"> GameTeamList to be changed Id </param>
        /// <param name="addGameTeamList"> GameTeamList entities to be added </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.GameTeamList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutGameTeamList( PublicApi.DTO.v1.AddGameTeamList addGameTeamList)
        {
            foreach (var player in addGameTeamList.PlayerList!.Where(x => x.PartOfGame))
            {
                var GTLEntity = new GameTeamList
                {
                    GameTeamId = addGameTeamList.GameTeamId,
                    TeamPersonId = player.PersonId,
                    InStartingLineup = player.InStartingLineup
                };
                _bll.GameTeamLists.Add(_gameTeamListMapper.Map(GTLEntity)!);
                await _bll.SaveChangesAsync();
            }

            if (addGameTeamList.StaffList != null)
            {
                foreach (var staff in addGameTeamList.StaffList!.Where(x => x.PartOfGame))
                {
                    var GTLEntity = new GameTeamList
                    {
                        GameTeamId = addGameTeamList.GameTeamId,
                        TeamPersonId = staff.PersonId,
                    };

                    _bll.GameTeamLists.Add(_gameTeamListMapper.Map(GTLEntity)!);
                    await _bll.SaveChangesAsync();
                }
            }

            return CreatedAtAction("Details", "Game"
                , new{id = (await _bll.GameTeams.FirstOrDefaultAsync(addGameTeamList.GameTeamId))!.GameId});
        }

        // POST: api/GameTeamList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 GameTeamList entity into Db
        /// </summary>
        /// <param name="gameTeamListMember">PublicApiVersion1.0 AddGameTeamListMember entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.AddGameTeamListMember), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.GameTeamList>> PostGameTeam_List(PublicApi.DTO.v1.AddGameTeamListMember gameTeamListMember)
        {
            gameTeamListMember.Person.AppUserId = User.GetUserId()!.Value;
            var GTL = new GameTeamList()
            {
                GameTeamId = gameTeamListMember.GameTeamId,
                Person = gameTeamListMember.Person,
                RoleId = gameTeamListMember.RoleId,
                Staff = true,
            };
            var returnEntity = _bll.GameTeamLists.Add(_gameTeamListMapper.Map(GTL)!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("Details", "Game", 
                new {id = (await _bll.GameTeams.FirstOrDefaultAsync(gameTeamListMember.GameTeamId))!.GameId }, returnEntity);
        }

        // DELETE: api/GameTeamList/5
        /// <summary>
        /// Delete GameTeamList entity given it's Id
        /// </summary>
        /// <param name="id"> GameTeamList's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteGame_Team_List(Guid id)
        {
            if (!await _bll.GameTeamLists.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.GameTeamLists.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
