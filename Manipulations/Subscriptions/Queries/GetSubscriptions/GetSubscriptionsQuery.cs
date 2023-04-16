using MediatR;
namespace Social_Network_API.Manipulations.Subscriptions.Queries.GetSubscriptions
{
    public class GetSubscriptionsQuery:IRequest<SubscriptionsVm>
    {
        public int Id { get; set; }
        public GetSubscriptionsQuery() { }
        public GetSubscriptionsQuery(int id) 
        {
            Id = id;
        }

    }
}
