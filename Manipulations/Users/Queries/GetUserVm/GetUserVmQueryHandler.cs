using MediatR;
using Social_Network_API.Database;
using Social_Network_API.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Entities;
using System.Security.Claims;
using System.Security.Principal;

namespace Social_Network_API.Manipulations.Users.Queries.GetUserVm
{
    public class GetUserVmQueryHandler : IRequestHandler<GetUserVmQuery, UserVm>
    {
        private readonly IMediator _mediator;
        private readonly MyDBContext _context;

        public GetUserVmQueryHandler(IMediator mediator, MyDBContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<UserVm> Handle(GetUserVmQuery request, CancellationToken cancellationToken)
        {
            if (request.Email != null)
            {
                if (!await _context.Users.AnyAsync(user => user.Email == request.Email))
                {
                    throw new NotFoundException("user", request.Email);
                }
                var response = await _context.Users.Where(user => user.Email == request.Email).FirstAsync();
                if (response != null)
                {
                    return new UserVm(response);
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
                        throw new NotFoundException("user", idToSearch);
                    }
                    var resultFromDb = await _context.Users.FirstOrDefaultAsync(user => user.Id == idToSearch);
                    if (resultFromDb != null)
                    {
                        return new UserVm(resultFromDb);
                    }
                    throw new DBException();
                }
                throw new NoIdentityException();
            }
            if (request.Id != null)
            {
                if (!await _context.Users.AnyAsync(user => user.Id == request.Id))
                {
                    throw new NotFoundException("user", request.Id);
                }
                var dbResponse = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.Id);
                if (dbResponse != null)
                {
                    return new UserVm(dbResponse);
                }
                throw new DBException();
            }
            throw new IncorectRequestException();
        }
    }
}


/*
 private User? GetCurrentUser()
        {

            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity != null)
            {

                var userClaims = identity.Claims;
                int idToSearch = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                

                if(!_context.Users.Any(x => x.Id == idToSearch))
                {
                    return null;
                }
                User? temp = _context.Users.Where(x => x.Id == idToSearch).ToArray().First();
                if(temp != null)
                {
                    return temp;
                }
                
            }
            return null;
        }
*/