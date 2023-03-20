using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Network_API.Entities;
using MediatR;
using Social_Network_API.Database;
using System.Security.Cryptography;
using Social_Network_API.Common.Exceptions;

namespace Social_Network_API.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler:IRequestHandler<CreateUserCommand, int>
    {
        MyDBContext _dbcontext;

        public CreateUserCommandHandler(MyDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<int> Handle(CreateUserCommand request,CancellationToken cancellationToken)
        {
            if(_dbcontext.Users.Any(user=>user.Email == request.Email))
            {
                throw new AlreadyExistException(request);
            }
            string salt = GenerateSalt();
            string hash = HashPassword(request.Password, salt);

            var tempUser = new User()
            {
                Name = request.Name,
                Age = request.Age,
                CreatedDate = DateTime.Now.ToFileTime(),
                Email = request.Email,
                Password = hash,
                Salt = salt
            };
            await _dbcontext.Users.AddAsync(tempUser);
            await _dbcontext.SaveChangesAsync();
            return tempUser.Id;
        }
        private string HashPassword(string password, string salt)
        {
            // Combine the password and salt values
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];
            Array.Copy(passwordBytes, combinedBytes, passwordBytes.Length);
            Array.Copy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

            // Generate the hash value using a cryptographic hash function
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        private string GenerateSalt()
        {
            // Generate a random salt value using a cryptographic random number generator
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
                
            }
            return Convert.ToBase64String(saltBytes);
        }
    }
}
