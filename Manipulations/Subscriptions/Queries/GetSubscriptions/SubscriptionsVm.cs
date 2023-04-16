using Social_Network_API.Entities;
using Social_Network_API.Manipulations.Users.Queries.GetUserVm;
namespace Social_Network_API.Manipulations.Subscriptions.Queries.GetSubscriptions
{
    public class SubscriptionsVm
    {
        public List<UserVm> Followers { get; set; } = new();
        public List<UserVm> Following { get; set; } = new();

        public SubscriptionsVm() { }

        public SubscriptionsVm(List<UserVm> followers, List<UserVm> following)
        {
            Followers = followers;
            Following = following;
        }

        public SubscriptionsVm(List<User> followers, List<User> following)
        {
            for (int i = 0; i < followers.Count; i++)
            {
                Followers.Add(new UserVm(followers[i]));
            }
            for (int i = 0; i < following.Count; i++)
            {
                Following.Add(new UserVm(following[i]));
            }
        }

        public SubscriptionsVm(User user)
        {
            for(int i = 0; i<user.Followers.Count; i++)
            {
                Followers.Add(new UserVm(user.Followers[i].Follower));
            }
            for(int i =0; i<user.Following.Count; i++)
            {
                Following.Add(new UserVm(user.Following[i].Following));
            }
        }
    }
}
