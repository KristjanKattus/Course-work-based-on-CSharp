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
    /// Api controller for EventTypes
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.EventTypeMapper _eventTypeMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of EventTypeMapper.
        /// </summary>
        /// <param name="mapper"> Automapper</param>
        /// <param name="bll"> Business layer </param>
        public EventTypeController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _eventTypeMapper = new EventTypeMapper(mapper);

        }

        // GET: api/EventType
        /// <summary>
        /// Get all EventType entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns> PublicApiVersion1.0 all EventType entities </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.EventType?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.EventType>>> GetEventTypes()
        {
            return Ok((await _bll.EventTypes.GetAllAsync(User.GetUserId()!.Value)).Select(x =>
                _eventTypeMapper.Map(x)));
        }

        // GET: api/EventType/5
        /// <summary>
        /// Get specific EventType which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id"> EventType unique Id</param>
        /// <returns> EventType entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.ClubTeam), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.EventType>> GetEvent_Type(Guid id)
        {
            var eventType = await _bll.EventTypes.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (eventType == null)
            {
                return NotFound();
            }

            return _eventTypeMapper.Map(eventType)!;
        }

        // PUT: api/EventType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update EventType entity
        /// </summary>
        /// <param name="id"> EventType to be changed Id</param>
        /// <param name="eventType"> EventType entity to be updated</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.EventType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutEvent_Type(Guid id, PublicApi.DTO.v1.EventType eventType)
        {
            if (id != eventType.Id)
            {
                return BadRequest();
            }

            if (!await _bll.EventTypes.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.EventTypes.Update(_eventTypeMapper.Map(eventType)!);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/EventType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 EventType entity into Db
        /// </summary>
        /// <param name="eventType"> PublicApiVersion1.0 EventType entity to be added </param>
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
        public async Task<ActionResult<PublicApi.DTO.v1.EventType>> PostEvent_Type(PublicApi.DTO.v1.EventType eventType)
        {
            var bllEntity = _eventTypeMapper.Map(eventType)!;
            _bll.EventTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.EventTypes.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _eventTypeMapper.Map(updatedEntity);

            return CreatedAtAction("GetEvent_Type", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/EventType/5
        /// <summary>
        /// Delete EventType entity given it's Id
        /// </summary>
        /// <param name="id"> EventType's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteEvent_Type(Guid id)
        {
            
            if (!await _bll.EventTypes.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.EventTypes.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
