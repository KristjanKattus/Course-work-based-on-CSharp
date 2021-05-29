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
    /// Api controller for Role
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicApi.DTO.v1.Mappers.RoleMapper _roleMapper;

        /// <summary>
        /// Constructor. Takes in IAppBll and automapper variant of roleMapper
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="bll">Business layer</param>
        public RoleController(IMapper mapper, IAppBLL bll)
        {
            _bll = bll;
            _roleMapper = new RoleMapper(mapper);
        }

        // GET: api/Role
        /// <summary>
        /// Get all Role entities in PublicApiVersion1.0.
        /// </summary>
        /// <returns>PublicApiVersion1.0 all Role entities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Role?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Role>>> GetFRoles()
        {
            return Ok((await _bll.Roles.GetAllAsync(User.GetUserId()!.Value))
                .Select(x => _roleMapper.Map(x)));
        }

        // GET: api/Role/5
        /// <summary>
        /// Get specific Role which matches the ID
        /// Can be accessed by authorized users.
        /// </summary>
        /// <param name="id">Role unique Id</param>
        /// <returns>Role entity of PublicApi.DTO.v1</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Role), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApi.DTO.v1.Role>> GetRole(Guid id)
        {

            var role = await _bll.Roles.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

            if (role == null)
            {
                return NotFound();
            }

            return _roleMapper.Map(role)!;
        }

        // PUT: api/Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Role entity
        /// </summary>
        /// <param name="id"> Role to be changed Id </param>
        /// <param name="role"> Role entity to be updated </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Role), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutRole(Guid id, PublicApi.DTO.v1.Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            if (!await _bll.Roles.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return BadRequest();
            }

            _bll.Roles.Update(_roleMapper.Map(role)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Role
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add PublicApi.DTO.v1 Role entity into Db
        /// </summary>
        /// <param name="role">PublicApiVersion1.0 Role entity to be added</param>
        /// <returns>Created Action with details of added entity</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Role), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Role>> PostRole(PublicApi.DTO.v1.Role role)
        {
            var bllEntity = _roleMapper.Map(role)!;
            _bll.Roles.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.Roles.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = _roleMapper.Map(updatedEntity);

            return CreatedAtAction("GetRole", new { id = returnEntity!.Id }, returnEntity);
        }

        // DELETE: api/Role/5
        /// <summary>
        /// Delete Role entity given it's Id
        /// </summary>
        /// <param name="id"> Role's Id to be deleted </param>
        /// <returns> NotFound if entity does not exist in Db </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            if (!await _bll.Roles.ExistsAsync(id, User.GetUserId()!.Value))
            {
                return NotFound();
            }

            await _bll.Roles.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
