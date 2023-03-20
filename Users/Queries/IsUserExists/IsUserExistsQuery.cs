using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Users.Queries.IsUserExists
{
    public class IsUserExistsQuery:IRequest<bool>
    {
        public readonly string? Email;
        public readonly int? Id;
        
        public IsUserExistsQuery(string email)
        {
            Email = email;
        }
        public IsUserExistsQuery(int id)
        {
            Id = id;
        }
       
    }
    
}
