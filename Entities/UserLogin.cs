using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Social_Network_API.Entities
{
    public class UserLogin
    {
        [BindRequired]
        [MaxLength(320)]
        [EmailAddress]
        public string? Email { get; set; }
        [BindRequired]
        [MinLength(8)]
        [MaxLength(64)]
        public string? Password { get; set; }
    }
}
