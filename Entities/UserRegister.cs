using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Social_Network_API.Entities
{
    public class UserRegister
    {
        //[Range(18,100)]
        public string Name { get; set; } = null!;
        //[MinLength(6)]
        //[MaxLength(64)]
        //[JsonIgnore]
        public string Password { get; set; } = null!;
        //[BindRequired]
        //[MaxLength(320)]
        //[EmailAddress]
        public string Email { get; set; } = null!;
    }
}
