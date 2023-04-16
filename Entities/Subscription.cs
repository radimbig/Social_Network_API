namespace Social_Network_API.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }

        public virtual User Follower { get; set; } = null!;
        public virtual User Following { get; set; } = null!;

        public Subscription(int followerId, int followingId)
        {
            FollowerId = followerId;
            FollowingId = followingId;
        }

        public Subscription(User follower, User following)
        {
            Follower = follower;
            Following = following;
        }

        public Subscription() { }
    }
}
