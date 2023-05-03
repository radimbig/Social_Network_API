using MediatR;
using Social_Network_API.Database;

namespace Social_Network_API.Manipulations.Database.SaveChanges
{
    public class SaveChangesQueryHandler : IRequestHandler<SaveChangesQuery>
    {
        private readonly MyDBContext _dbContext;
        public SaveChangesQueryHandler(MyDBContext dBContext) { _dbContext = dBContext; }
        public async Task<Unit> Handle(SaveChangesQuery request, CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
