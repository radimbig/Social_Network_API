using MediatR;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Database;

namespace Social_Network_API.Manipulations.Users.Queries.GetUsersCount
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersCountQuery, int>
    {
        public readonly MyDBContext _dbContext;
        public GetUsersQueryHandler(MyDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<int> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.CountAsync();
        }
    }
}
