using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Social_Network_API.Enums;
using Social_Network_API.Common.Exceptions;
using MediatR;
using Social_Network_API.Manipulations.Users.Queries.GetUserVm;
using Social_Network_API.Manipulations.Users.Queries.GetUsersCount;
using Social_Network_API.Manipulations.Users.Queries.GetUsers;
using Social_Network_API.Manipulations.Users.Commands.DeleteUser;
using Social_Network_API.Manipulations.Users.Queries.GetUser;

namespace Social_Network_API.Controllers
{
    [Route("user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            return Ok(await _mediator.Send(new GetUserVmQuery(this.HttpContext)));
        }

        [AllowAnonymous]
        [Route("{id?}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return (Ok(await _mediator.Send(new GetUserVmQuery(id))));
        }

        [AllowAnonymous]
        [Route("count")]
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            return Ok(await _mediator.Send(new GetUsersCountQuery()));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetList([FromQuery] int index, int count)
        {
            return Ok(await _mediator.Send(new GetUsersQuery(count, index)));
        }

        [Authorize]
        [Route("{id?}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if ((await _mediator.Send(new GetUserQuery(this.HttpContext))).Role != UserRole.Admin)
            {
                throw new NoPermissionException();
            }
            await _mediator.Send(new DeleteUserCommand(id));
            return Ok();
        }
    }
}


