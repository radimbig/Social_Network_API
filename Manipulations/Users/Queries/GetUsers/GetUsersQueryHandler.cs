using MediatR;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Database;
using Social_Network_API.Manipulations.Users.Queries.GetUserVm;

namespace Social_Network_API.Manipulations.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserVm>>
    {
        private readonly MyDBContext _dbContext;
        public GetUsersQueryHandler(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<UserVm>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {


            var dbResponse = await _dbContext.Users.OrderByDescending(e => e.Id).Skip(request.Index).Take(request.Count).ToArrayAsync();
            if (dbResponse.Length == 0)
            {
                return new List<UserVm>();
            }
            List<UserVm> users = new(dbResponse.Length);

            foreach (var user in dbResponse)
            {
                users.Add(new UserVm(user));
            }
            return users;

        }
    }
}
