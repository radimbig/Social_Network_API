using MediatR;

namespace Social_Network_API.Manipulations.Users.Commands.AddUserAvatar
{
    public class AddUserAvatarCommand : IRequest<bool>
    {
        public int TargetId;
        public IFormFile Avatar { get; set; }

        public AddUserAvatarCommand(IFormFile avatar, int targetId)
        {
            Avatar = avatar;
            TargetId = targetId;
        }
    }
}
