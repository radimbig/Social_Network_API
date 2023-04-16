using MediatR;
using Social_Network_API.Database;

using Social_Network_API.Common.Exceptions;
using Social_Network_API.Manipulations.Users.Queries.IsUserExists;
using Social_Network_API.Manipulations.Users.Queries.GetUser;

namespace Social_Network_API.Manipulations.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        public readonly MyDBContext _dBContext;
        public readonly IMediator _mediator;

        public DeleteUserCommandHandler(MyDBContext dbContext, IMediator mediator)
        {
            _dBContext = dbContext;
            _mediator = mediator;
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _mediator.Send(new IsUserExistsQuery(request.Id)))
            {
                throw new NotFoundException("user", request.Id);
            }
            var target = await _mediator.Send(new GetUserQuery(request.Id));
            if (target == null)
            {
                throw new DBException();
            }
            _dBContext.Users.Remove(target);
            await _dBContext.SaveChangesAsync();
            return true;
        }
    }
}
