using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Users.Queries.IsUserExists
{
    public class IsUserExistsQuerie:IRequest<bool>
    {
        public string Email { get; set; } = null!;
        public IsUserExistsQuerie(string email)
        {
            Email = email;
        }
        public IsUserExistsQuerie() { }
    }
    
}
