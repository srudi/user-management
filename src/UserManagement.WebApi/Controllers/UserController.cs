using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common;
using UserManagement.Application.Users.Commands.Create;
using UserManagement.Application.Users.Commands.Delete;
using UserManagement.Application.Users.Commands.Update;
using UserManagement.Application.Users.Dtos;
using UserManagement.Application.Users.Queries.Get;
using UserManagement.Application.Users.Queries.GetAll;
using UserManagement.Domain.Entities;
using UserManagement.WebAPI.Controllers.Common;

namespace UserManagement.WebAPI.Controllers
{
    public class UserController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll(int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            var pageInfo = new PageInfo(pageSize: pageSize, pageIndex: pageIndex);
            var users = await Mediator.Send(new GetAllPagedQuery(pageInfo), cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(long id, CancellationToken cancellationToken)
        {
            var user = await Mediator.Send(new GetQuery(id), cancellationToken);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserDto user, CancellationToken cancellationToken)
        {
            var createdUser = await Mediator.Send(new CreateCommand(user), cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut]
        public async Task<ActionResult> Update(long id, [FromBody] UserDto user, CancellationToken cancellationToken)
        {
            if (id != user.Id)
                return BadRequest("The \"id\" has to match with \"user.id\".");

            await Mediator.Send(new UpdateCommand(user), cancellationToken);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteCommand(id), cancellationToken); ;
            return NoContent();
        }
    }
}
