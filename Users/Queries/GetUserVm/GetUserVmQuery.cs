using MediatR;
using Social_Network_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Users.Queries.GetUserVm
{
    public class GetUserVmQuery:IRequest<UserVm>
    {
        public readonly string? Email;
        public readonly int? Id;
        public readonly HttpContext? Context;

        
        public GetUserVmQuery(int id)
        {
            Id = id;
        }
        public GetUserVmQuery(string email)
        {
            Email = email;
        }
        public GetUserVmQuery(HttpContext context)
        {
            Context = context;
        }
    }
}
