using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Database;
using Social_Network_API.Entities;

namespace Social_Network_API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthorizationController:ControllerBase
    {
        private readonly MyDBContext _context;



        public AuthorizationController(MyDBContext context)
        {
            _context = context;

        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login()
        {
            return Ok();
        }

        [Route("register")]
        [HttpPost]
        [HttpPost]
        public IActionResult Post([FromForm] User user)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var tempUser = new User(user.Name, user.Email, user.Age, DateTime.Now, user.Password);
            if (_context.Users.Any(e => e.Email == user.Email))
            {
                HttpContext.Response.StatusCode = 409;
                return new JsonResult(new { description = "User with this email already exists" });
            }

            _context.Users.Add(tempUser);
            _context.SaveChanges();

            return new JsonResult(tempUser);
        }

    }
}
