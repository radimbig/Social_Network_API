using Social_Network_API.Entities;

namespace Social_Network_API.Manipulations.Users.Queries.GetUserVm
{
    public class UserVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool HasAvatar { get; set; } = false;
        public int Age { get; set; }

        public UserVm(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Age = user.Age;
            HasAvatar = File.Exists(Directory.GetCurrentDirectory() + "/StaticFiles/Avatars/" + Id.ToString() + ".jpg");
        }
    }
}
