using Social_Network_API.Enums;
using System.Text.Json;


namespace Social_Network_API.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password = null!;

        public string Salt { get; set; } = null!;

        public int Age { get; set; }

        public UserRole Role { get; set; }

        public long CreatedDate { get; set; }

        public List<Subscription> Following { get; set; } = new();
        public List<Subscription> Followers { get; set; } = new();

        public User(
            string name,
            string email,
            int age,
            DateTime createDate,
            string password,
            string salt
        )
        {
            Password = password;
            Name = name;
            Email = email;
            Age = age;
            CreatedDate = createDate.ToFileTime();
            Role = UserRole.User;
            Salt = salt;
        }

        public User()
        {
            Role = UserRole.User;
        }

        public override string ToString()
        {
            string output = JsonSerializer.Serialize(this);
            return output;
        }
    }
}


/*

using (var sha256 = SHA256.Create())
{
    byte[] buffer = Encoding.UTF8.GetBytes(password);
    Password = Convert.ToBase64String(sha256.ComputeHash(buffer));
    Name = name;
    Email = email;
    Age = age;
    CreatedDate = createDate.ToUniversalTime().ToFileTimeUtc();
}

*/
