using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Database;
using Social_Network_API.Entities;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Social_Network_API.Commands.Users.CreateUser;
using Social_Network_API.Users.Queries.IsUserExists;
using Social_Network_API.Auth.Login.IsLoginPairCorrect;
using Social_Network_API.Auth.Login.GetToken;

namespace Social_Network_API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthorizationController:ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;


        public AuthorizationController(MyDBContext context, IConfiguration config, IMediator mediator)
        {
            _context = context;
            _config = config;
            _mediator = mediator;
        }




        /*
          if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }
            return NotFound($"No user with email:{userLogin.Email} and password:{userLogin.Password}");
         */

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if(!await _mediator.Send(new IsLoginPairCorrectQuery(userLogin))){
                return BadRequest($"No user with email:{userLogin.Email} and password:{userLogin.Password}");
            }
            string token = await _mediator.Send(new GetTokenQuery(userLogin));
            return Ok(token);
        }
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        
        public async Task<IActionResult> Register([FromForm] UserRegister user)
        {

            if(await _mediator.Send(new IsUserExistsQuery(user.Email)))
            {
                return BadRequest($"User with email:'{user.Email}' already exists");
            }
            await _mediator.Send(new CreateUserCommand()
            {
                    Name = user.Name,
                    Age = user.Age,
                    Email = user.Email,
                    Password = user.Password
            });
            return Ok();

        }

        private User? Authenticate(UserLogin user)
        {
            
            User? currentUser = _context.Users.OrderByDescending(e => e.Id).FirstOrDefault(e => e.Email == user.Email);


            if (currentUser == null)
            {
                Console.WriteLine("current user = null");
            }
            Console.WriteLine(currentUser?.ToString());
            if (currentUser != null)
            {
                var hashedPassword = HashPassword(user.Password, currentUser.Salt);
                if (hashedPassword != currentUser.Password)
                {
                    return null;
                }
                return currentUser;
            }
                
            
            return null;
            
            
        }
        
        private string GenerateToken(User user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            

            var token = new JwtSecurityToken(_config.GetValue<string>("Jwt:Issuer"),
                _config.GetValue<string>("Jwt:Audience"),
                claims, expires:DateTime.Now.AddDays(1),
                signingCredentials:credentials
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
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
