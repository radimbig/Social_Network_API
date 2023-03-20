using MediatR;
using Social_Network_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Auth.Login.IsLoginPairCorrect
{
    public class IsLoginPairCorrectQuery:IRequest<bool>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;


        public IsLoginPairCorrectQuery() { }
        public IsLoginPairCorrectQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public IsLoginPairCorrectQuery(UserLogin user)
        {
            Email = user.Email;
            Password = user.Password;
        }
    }
}
