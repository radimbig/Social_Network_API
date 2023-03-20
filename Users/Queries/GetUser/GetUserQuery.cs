using MediatR;
using Social_Network_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Users.Queries.GetUser
{
    public class GetUserQuery:IRequest<UserVm>
    {
        public readonly string? Email;
        public readonly int? Id;
        public readonly HttpContext? Context;

        
        public GetUserQuery(int id)
        {
            Id = id;
        }
        public GetUserQuery(string email)
        {
            Email = email;
        }
        public GetUserQuery(HttpContext context)
        {
            Context = context;
        }
    }
}
