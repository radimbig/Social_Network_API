using MediatR;
using ImageMagick;
using Social_Network_API.Manipulations.Users.Queries.GetUser;
using Social_Network_API.Manipulations.Database.SaveChanges;

namespace Social_Network_API.Manipulations.Users.Commands.AddUserAvatar
{
    public class AddUserAvatarCommandHandler : IRequestHandler<AddUserAvatarCommand>
    {
        private readonly string RelativePathForImages = "/StaticFiles/Avatars/";
        private readonly IMediator _mediator;

        public AddUserAvatarCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
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
            var target = await _mediator.Send(new GetUserQuery(request.TargetId));
            target.HasAvatar = true;
            await _mediator.Send(new SaveChangesQuery());
            return Unit.Value;
        }
    }
}
