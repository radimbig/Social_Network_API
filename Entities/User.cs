using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Social_Network_API.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using System.Text.Json;

namespace Social_Network_API.Entities {
    public class User
    {



        [BindNever]

        public int Id { get; set; }


        [BindRequired]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [BindRequired]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; } = null!;


        [MinLength(6)]
        [MaxLength(64)]
        [JsonIgnore]
        
        public string Password = null!;


        [BindNever]
        [JsonIgnore]
        [MaxLength(24)]

        public string Salt { get; set; } = null!;

        [BindRequired]
        [Range(18, 100)]
        public int Age { get; set; }
        [JsonIgnore]
        [BindNever]
        public UserRole Role { get; set; }
        
        public long CreatedDate { get; set; }
        public User(string name, string email, int age, DateTime createDate, string password, string salt)
        {
            Password = password;
            Name = name;
            Email = email;
            Age = age;
            CreatedDate = createDate.ToFileTime();
            Role = UserRole.User;
            Salt = salt;
        }
        public User() {
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