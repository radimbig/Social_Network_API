using MediatR;
using Social_Network_API.Users.Queries.GetUserVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Users.Queries.GetUsers
{
    public class GetUsersQuery:IRequest<IEnumerable<UserVm>>
    {
        public int Count = 10;
        public int Index = 0;
        public GetUsersQuery() {}
        public GetUsersQuery(int count, int index) { (Count, Index) = (count, index); }
    }
}
