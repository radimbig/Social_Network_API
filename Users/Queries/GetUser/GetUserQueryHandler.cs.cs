using MediatR;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Common.Exceptions;
using Social_Network_API.Database;
using Social_Network_API.Entities;
using System.Security.Claims;
namespace Social_Network_API.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IMediator _mediator;
        private readonly MyDBContext _context;

        public GetUserQueryHandler(IMediator mediator, MyDBContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Email != null)
            {
                if (!await _context.Users.AnyAsync(user => user.Email == request.Email))
                {
                    throw new NotFoundException("User", request.Email);
                }
                var response = await _context.Users.Where(user => user.Email == request.Email).FirstAsync();
                if (response != null)
                {
                    return response;
                }
                throw new DBException();

            }
            if (request.Context != null)
            {
                ClaimsIdentity? identity = request.Context.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userClaims = identity.Claims;

                    int idToSearch = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                    if (!await _context.Users.AnyAsync(user => user.Id == idToSearch))
                    {
                        throw new NotFoundException("User", idToSearch);
                    }
                    var resultFromDb = await _context.Users.FirstOrDefaultAsync(user => user.Id == idToSearch);
                    if (resultFromDb != null)
                    {
                        return resultFromDb;
                    }
                    throw new DBException();
                }
                throw new NoIdentityException();
            }
            if (request.Id != null)
            {
                if (!await _context.Users.AnyAsync(user => user.Id == request.Id))
                {
                    throw new NotFoundException("User", request.Id);
                }
                var dbResponse = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.Id);
                if (dbResponse != null)
                {
                    return dbResponse;
                }
                throw new DBException();
            }
            throw new IncorectRequestException();
        }
    }
}
