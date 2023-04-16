using MediatR;
using Social_Network_API.Database;
using Social_Network_API.Common.Exceptions;
using Social_Network_API.Manipulations.Users.Queries.IsUserExists;
using Social_Network_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Social_Network_API.Manipulations.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, bool>
    {
        private readonly MyDBContext dBContext;
        private readonly IMediator mediator;

        public CreateSubscriptionCommandHandler(MyDBContext dBContext, IMediator mediator)
        {
            this.dBContext = dBContext;
            this.mediator = mediator;
        }

        public async Task<bool> Handle(
            CreateSubscriptionCommand request,
            CancellationToken cancellationToken
        )
        {
            if(await dBContext.Subscriptions.AnyAsync(s =>s.FollowerId == request.requester && s.FollowingId == request.target, cancellationToken: cancellationToken))
            {
                return true;
            }
            if(!(await mediator.Send(new IsUserExistsQuery(request.requester))))
            {
                throw new NotFoundException("User", request.requester);
            }
            if (!(await mediator.Send(new IsUserExistsQuery(request.target), cancellationToken)))
            {
                throw new NotFoundException("User", request.target);
            }
            await dBContext.Subscriptions.AddAsync(new Subscription(request.requester, request.target), cancellationToken);
            dBContext.SaveChanges();
            return true;
        }
    }
}
