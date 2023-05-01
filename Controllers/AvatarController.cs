using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Manipulations.Users.Commands.AddUserAvatar;
using Social_Network_API.Manipulations.Users.Queries.GetUser;

namespace Social_Network_API.Controllers
{
    [Route("upload/avatar")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AvatarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAvatar(IFormFile avatar)
        {
            var user = await _mediator.Send(new GetUserQuery(HttpContext));
            await _mediator.Send(new AddUserAvatarCommand(avatar, user.Id));
            return Ok();
        }
    }
}
