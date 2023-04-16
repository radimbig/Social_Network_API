using FluentValidation;

namespace Social_Network_API.Manipulations.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
