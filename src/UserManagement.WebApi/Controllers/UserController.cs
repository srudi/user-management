using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common;
using UserManagement.Application.Services;
using UserManagement.Application.Users.Queries;
using UserManagement.Application.Users.Queries.GetAll;
using UserManagement.Domain.Entities;

namespace UserManagement.WebAPI.Controllers
{
    [Produces("application/json")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
            
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> GetAll(int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            var pageInfo = new PageInfo(pageSize: pageSize, pageIndex: pageIndex);
            var users = await Mediator.Send(new GetAllPagedQuery(pageInfo), cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> Get(long id)
        {
            var user = await _userService.Get(id);
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] User user)
        {
            var userId = await _userService.Create(user);
            return CreatedAtAction(nameof(Get), new { id = userId }, user);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(long id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("The \"id\" has to match with \"user.id\".");

            await _userService.Update(user);
            return Accepted();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(long id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}
