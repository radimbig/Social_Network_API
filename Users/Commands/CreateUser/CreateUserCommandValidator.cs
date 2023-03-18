using FluentValidation;
using Social_Network_API.Commands.Users.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public  CreateUserCommandValidator()
        {
            RuleFor(createUserCommand => createUserCommand.Name).NotEmpty().NotNull().MinimumLength(4).MaximumLength(15);
            RuleFor(createUserCommand => createUserCommand.Password).NotEmpty().NotNull().MinimumLength(8).MaximumLength(30);
            RuleFor(createUserCommand => createUserCommand.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(createUserCommand => createUserCommand.Age).NotNull().NotEmpty().InclusiveBetween(18, 100);
        }
    }
}
