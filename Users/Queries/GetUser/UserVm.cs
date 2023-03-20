using Social_Network_API.Entities;

namespace Social_Network_API.Users.Queries.GetUser
{
    public class UserVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public int Age { get; set; } 

        public UserVm() { }
        public UserVm(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Age = user.Age;
        }
    }
}
