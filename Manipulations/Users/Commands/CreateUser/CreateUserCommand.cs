using MediatR;
using Social_Network_API.Entities;

namespace Social_Network_API.Manipulations.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        public int Age { get; set; }

        public CreateUserCommand() { }
        public CreateUserCommand(UserRegister user)
        {
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
        }
    }
}
