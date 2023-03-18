using MediatR;
using Social_Network_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Commands.Users.CreateUser
{
    public class CreateUserCommand:IRequest<int>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        public int Age { get; set; }

        
    }
}
