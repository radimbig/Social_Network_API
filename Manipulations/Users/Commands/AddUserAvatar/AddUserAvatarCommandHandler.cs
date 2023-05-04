using MediatR;
using ImageMagick;
using Social_Network_API.Common.Exceptions;
using Social_Network_API.Database;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Entities;

namespace Social_Network_API.Manipulations.Users.Commands.AddUserAvatar
{
    public class AddUserAvatarCommandHandler : IRequestHandler<AddUserAvatarCommand>
    {
        private readonly string RelativePathForImages = "/StaticFiles/Avatars/";

        private readonly MyDBContext _dbContext;
        public AddUserAvatarCommandHandler(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(
            AddUserAvatarCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var image = new MagickImage(request.Avatar.OpenReadStream()))
            {
                image.Resize(
                    new MagickGeometry
                    {
                        Width = 300,
                        Height = 300,
                        IgnoreAspectRatio = true,
                    }
                );
                image.Format = MagickFormat.Jpeg;
                await image.WriteAsync(
                    Directory.GetCurrentDirectory()
                        + RelativePathForImages
                        + request.TargetId.ToString()
                        + ".jpg"
                );
            }
            var target = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.TargetId, cancellationToken: cancellationToken);
            if( target == null )
            {
                throw new NotFoundException("User", request.TargetId);
            }
            target.HasAvatar = true;
            await _dbContext.SaveChangesAsync(cancellationToken);         
            return Unit.Value;
        }
    }
}
