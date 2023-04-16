using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Manipulations.Subscriptions.Commands.CreateSubscription;
using Social_Network_API.Manipulations.Subscriptions.Commands.DeleteSubscription;
using Social_Network_API.Manipulations.Subscriptions.Queries.GetSubscriptions;
using Social_Network_API.Manipulations.Users.Queries.GetUser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Social_Network_API.Controllers
{
    [Route("subscriptions")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var requester = await _mediator.Send(new GetUserQuery(HttpContext));
            return Ok((await _mediator.Send(new GetSubscriptionsQuery(requester.Id))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetSubscriptionsQuery(id)));
        }

       
        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            var requester = await _mediator.Send(new GetUserQuery(HttpContext));
            await _mediator.Send(new CreateSubscriptionCommand(requester.Id, id));
            return Ok();
        }

        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var requester = await _mediator.Send(new GetUserQuery(HttpContext));
            await _mediator.Send(new DeleteSubscriptionCommand(requester.Id, id));
            return Ok();
        }
    }
}
