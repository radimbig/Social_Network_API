using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Social_Network_API.Database;
using Social_Network_API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Social_Network_API.Common.Exceptions;


namespace Social_Network_API.Auth.Login.GetToken
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, string>
    {
        private readonly MyDBContext _dbcontext;
        private readonly IConfiguration _config;
        
        
        public GetTokenQueryHandler(MyDBContext context, IConfiguration configuration)
        {
            _dbcontext = context;
            _config = configuration;
           
            
        }
        public async Task<string> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var target = await _dbcontext.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (target == null)
            {
                throw new NoTokenForPairException();
            }
            string token = GenerateToken(target);
            return token;
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
                claims, expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

