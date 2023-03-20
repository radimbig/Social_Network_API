using MediatR;
using Social_Network_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Auth.Login.GetToken
{
    public class GetTokenQuery:IRequest<string>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public GetTokenQuery() { }
        public GetTokenQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public GetTokenQuery(UserLogin user)
        {
            Email = user.Email;
            Password = user.Password;
        }
    }
    
}
