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
    /// Api controller for Person
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class PersonController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.PersonMapper _personMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of personMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public PersonController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _personMapper = new PersonMapper(mapper);
        }

        // GET: api/Person
        /// <summary>
        /// Get all Person entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all Person entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Person?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Person>>> GetPersons()
        {
            return Ok((await _bll.Persons.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _personMapper.Map(x)));
        }

        // GET: api/Person/5
        /// <summary>
        /// Get specific Person which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">Person unique Id</param>
        /// <returns>Person entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.Person>> GetPerson(Guid id)
        {
            var person = await _bll.Persons.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (person == null)
            {
                return NotFound();
            }

            return _personMapper.Map(person)!;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Person entity
        /// </summary>
        /// <param name="id"> Person to be changed Id </param>
        /// <param name="person"> Person entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutPerson(Guid id, PublicApi.DTO.v1.Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }
            
            if (!await _bll.Persons.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.Persons.Update(_personMapper.Map(person)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 Person entity into Db
        /// </summary>
        /// <param name="person">PublicApiVersion1.0 Person entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PublicApi.DTO.v1.Person>> PostPerson(PublicApi.DTO.v1.Person person)
        {
            var bllEntity = _personMapper.Map(person)!;
            _bll.Persons.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Persons.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _personMapper.Map(updatedEntity);

            return CreatedAtAction("GetPerson", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/Person/5
        /// <summary>
        /// Delete Person entity given it's Id
        /// </summary>
        /// <param name="id"> Person's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            if (!await _bll.Persons.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.Persons.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
