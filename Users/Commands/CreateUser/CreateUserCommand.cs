using MediatR;
using Social_Network_API.Entities;

namespace Social_Network_API.Commands.Users.CreateUser
{
    public class CreateUserCommand:IRequest<int>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        public int Age { get; set; }

        public CreateUserCommand() { }
        public CreateUserCommand(UserRegister user)
        {
            Name = user.Name;
            Age = user.Age;
            Email = user.Email;
            Password = user.Password;
        }
    }
}
