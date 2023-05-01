using MediatR;
using Social_Network_API.Database;
using Social_Network_API.Manipulations.Subscriptions.Queries.GetSubscriptions;
using Social_Network_API.Manipulations.Users.Queries.GetUserVm;

namespace Social_Network_API.Manipulations.Subscriptions.Queries.GetFriends
{
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, List<UserVm>>
    {
        private readonly IMediator _mediator;

        public GetFriendsQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<UserVm>> Handle(
            GetFriendsQuery request,
            CancellationToken cancellationToken
        )
        {
            var subscriptions = await _mediator.Send(new GetSubscriptionsQuery(request.Id));
            List<UserVm> result = new List<UserVm>();
            for (int i = 0; i < subscriptions.Following.Count; i++)
            {
                if (subscriptions.Followers.Any(u => u.Email == subscriptions.Following[i].Email))
                {
                    result.Add(subscriptions.Following[i]);
                }
            }
            return result;
        }
    }
}
