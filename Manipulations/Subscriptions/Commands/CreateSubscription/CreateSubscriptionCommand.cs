using MediatR;
using Social_Network_API.Entities;

namespace Social_Network_API.Manipulations.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommand:IRequest<bool>
    {
        public int requester { get; set; }
        public int target { get; set; }

        public CreateSubscriptionCommand() { }
        public CreateSubscriptionCommand(int requester, int target)
        {
            this.requester = requester;
            this.target = target;
        }
        public CreateSubscriptionCommand(User requester, User target)
        {
            this.requester = requester.Id;
            this.target = target.Id;
        }
    }
}
