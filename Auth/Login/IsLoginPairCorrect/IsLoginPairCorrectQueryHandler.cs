using MediatR;
using System.Security.Cryptography;
using Social_Network_API.Database;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Social_Network_API.Users.Queries.IsUserExists;
using Social_Network_API.Common.Exceptions;

namespace Social_Network_API.Auth.Login.IsLoginPairCorrect
{
    public class IsLoginPairCorrectQueryHandler:IRequestHandler<IsLoginPairCorrectQuery, bool>
    {
        private MyDBContext _dbcontext;
        
        public IsLoginPairCorrectQueryHandler(MyDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<bool> Handle(IsLoginPairCorrectQuery request, CancellationToken cancellationToken)
        {
            if(!await _dbcontext.Users.AnyAsync(user=>user.Email == request.Email))
            {
                return false;
            }
            var target = await _dbcontext.Users.FirstOrDefaultAsync(user=>user.Email == request.Email);
            if(target == null)
            {
                throw new DBException();
            }
            string requestHash = HashPassword(request.Password, target.Salt);
            return requestHash == target.Password;
            
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
    }
}
