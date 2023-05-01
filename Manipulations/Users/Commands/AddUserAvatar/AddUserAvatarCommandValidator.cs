using FluentValidation;
using Social_Network_API.Models;

namespace Social_Network_API.Manipulations.Users.Commands.AddUserAvatar
{
    public class AddUserAvatarCommandValidator:AbstractValidator<AddUserAvatarCommand>
    {
        
        public AddUserAvatarCommandValidator()
        {
            RuleFor(o => o.Avatar).Must(ImageValidator.IsImageValid).WithMessage("Image is not valid");
            RuleFor(o => o.TargetId).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
