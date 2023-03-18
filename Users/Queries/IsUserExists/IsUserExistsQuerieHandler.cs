
using MediatR;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Database;


namespace Social_Network_API.Users.Queries.IsUserExists
{
    public class IsUserExistsQuerieHandler:IRequestHandler<IsUserExistsQuerie, bool>
    {
        MyDBContext _dbcontext;

        public IsUserExistsQuerieHandler(MyDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<bool> Handle(IsUserExistsQuerie request, CancellationToken cancellationToken)
        {
            return await _dbcontext.Users.AnyAsync(user => user.Email == request.Email);
        }
    }
}
