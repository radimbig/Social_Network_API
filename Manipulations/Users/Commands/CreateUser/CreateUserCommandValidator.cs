﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Manipulations.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(createUserCommand => createUserCommand.Name).NotEmpty().NotNull().MinimumLength(4).MaximumLength(15);
            RuleFor(createUserCommand => createUserCommand.Password).NotEmpty().NotNull().MinimumLength(8).MaximumLength(30);
            RuleFor(createUserCommand => createUserCommand.Email).NotEmpty().NotNull().EmailAddress();
            
        }
    }
}
