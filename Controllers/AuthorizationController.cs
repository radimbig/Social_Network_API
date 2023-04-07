using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Database;
using Social_Network_API.Entities;
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


        


        
      

 
    }
}
