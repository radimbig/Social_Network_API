using MediatR;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Database;
using Social_Network_API.Common.Exceptions;

namespace Social_Network_API.Manipulations.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, bool>
    {
        private readonly MyDBContext dBContext;

        public DeleteSubscriptionCommandHandler(MyDBContext myDB)
        {
            dBContext = myDB;
        }
        public async Task<bool> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            if (!(await dBContext.Subscriptions.AnyAsync(s=>s.FollowerId == request.FollowerId && s.FollowingId == request.FollowingId)))
            {
                throw new NotFoundException($"No subsription of {request.FollowerId} to {request.FollowingId}");
            }

            var sub =  await dBContext.Subscriptions.FirstOrDefaultAsync(s =>s.FollowerId == request.FollowerId && s.FollowingId == request.FollowingId, cancellationToken: cancellationToken);

            if (sub == null)
            {
                throw new DBException();
            }
            dBContext.Subscriptions.Remove(sub);
            await dBContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
