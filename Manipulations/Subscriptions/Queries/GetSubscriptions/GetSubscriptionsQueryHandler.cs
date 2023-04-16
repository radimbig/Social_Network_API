using MediatR;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Common.Exceptions;
using Social_Network_API.Database;
using Social_Network_API.Manipulations.Users.Queries.IsUserExists;
using System.Text.Json;

namespace Social_Network_API.Manipulations.Subscriptions.Queries.GetSubscriptions
{
    public class GetSubscriptionsQueryHandler
        : IRequestHandler<GetSubscriptionsQuery, SubscriptionsVm>
    {
        private readonly IMediator mediator;
        private readonly MyDBContext dbContext;

        public GetSubscriptionsQueryHandler(IMediator mediator, MyDBContext dBContext)
        {
            this.mediator = mediator;
            this.dbContext = dBContext;
        }

        public async Task<SubscriptionsVm> Handle(
            GetSubscriptionsQuery request,
            CancellationToken cancellationToken
        )
        {
            if (!(await mediator.Send(new IsUserExistsQuery(request.Id), cancellationToken)))
            {
                throw new NotFoundException("User", request.Id);
            }
            var user = await dbContext.Users
                .Include(u => u.Followers)
                .ThenInclude(s => s.Follower)
                .Include(u => u.Following)
                .ThenInclude(s => s.Following)
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken: cancellationToken);
            if (user == null)
            {
                throw new DBException();
            }
            return new SubscriptionsVm(user);
        }
    }
}
