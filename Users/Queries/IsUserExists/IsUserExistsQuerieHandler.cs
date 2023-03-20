
using MediatR;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Database;


namespace Social_Network_API.Users.Queries.IsUserExists
{
    public class IsUserExistsQuerieHandler:IRequestHandler<IsUserExistsQuery, bool>
    {
        MyDBContext _dbcontext;

        public IsUserExistsQuerieHandler(MyDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<bool> Handle(IsUserExistsQuery request, CancellationToken cancellationToken)
        {
            if(request.Email != null)
            {
                return await _dbcontext.Users.AnyAsync(user => user.Email == request.Email);
            }
            if(request.Id != null)
            {
                return await _dbcontext.Users.AnyAsync(user => user.Id == request.Id);
            }
            return false;
        }
    }
}
