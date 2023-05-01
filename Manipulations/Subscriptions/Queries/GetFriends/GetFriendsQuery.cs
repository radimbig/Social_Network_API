using MediatR;
using Social_Network_API.Manipulations.Users.Queries.GetUserVm;

namespace Social_Network_API.Manipulations.Subscriptions.Queries.GetFriends
{
    public class GetFriendsQuery:IRequest<List<UserVm>>
    {
        public int Id { get; set; }
        public GetFriendsQuery(int id) { Id = id; }
    }
}
