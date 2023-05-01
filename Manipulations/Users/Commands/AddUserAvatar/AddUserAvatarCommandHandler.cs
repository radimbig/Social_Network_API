using MediatR;
using ImageMagick;

namespace Social_Network_API.Manipulations.Users.Commands.AddUserAvatar
{
    public class AddUserAvatarCommandHandler : IRequestHandler<AddUserAvatarCommand, bool>
    {
        private readonly string RelativePathForImages = "/StaticFiles/Avatars/";

        public async Task<bool> Handle(
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
            return true;
        }
    }
}
