using MediatR;

namespace Social_Network_API.Manipulations.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommand:IRequest<bool>
    {
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }

        public DeleteSubscriptionCommand(int followerId, int followingId)
        {
            FollowerId = followerId;
            FollowingId = followingId;
        }
    }
}
