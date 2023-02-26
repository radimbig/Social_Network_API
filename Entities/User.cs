using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Social_Network_API.Enums;
namespace Social_Network_API.Entities {
    public class User
    {

        

        [BindNever]
        
        public int Id { get; set; }


        [BindRequired]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [BindRequired]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [MinLength(6)]
        [MaxLength(64)]
        [JsonIgnore]
        public string Password = string.Empty;
        [BindRequired]
        [Range(18,100)]
        public int Age { get; set; }
        [JsonIgnore]
        public UserRole Role = UserRole.User;
        
        public long CreatedDate { get; set; }
        public User(string name, string email, int age, DateTime createDate, string password)
        {
            var sha256 = SHA256.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            Password = Convert.ToBase64String(sha256.ComputeHash(buffer));
            Name = name;
            Email = email;
            Age = age;

            CreatedDate = createDate.ToUniversalTime().ToFileTimeUtc();
            
        }
        public User() { }
    }
}