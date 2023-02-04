using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Entities;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Social_Network_API.Models;


namespace Social_Network_API.Controllers
{

    [Route("user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> Users = new List<User>();
        public Logger logger;
        [HttpPost]
        public IActionResult Post([FromForm] User user)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            var tempUser = new User(user.Name, user.Email, user.Age, DateTime.Now);
            Users.Add(tempUser);
            logger = (Logger)(HttpContext.Items["logger"]);
            logger.AddUser(tempUser);
            return new JsonResult(Users.Last());
        }
        [Route("{id?}")]
        [HttpGet]
        public IActionResult Get([FromRoute] string id)
        {
            var tempUser = Users.FirstOrDefault(e => { return e.Id == id; });
            if (tempUser == null)
            {
                return new JsonResult(new { error = 404, description = $"Cant find user with {id} id" });
            }
            return new JsonResult(tempUser);
        }
        [Route("count")]
        [HttpGet]
        public IActionResult Count()
        {
            return new JsonResult(new { TotalUsersCount = Entities.User.countOfUsers });
        }

        [HttpGet]

        public IActionResult Get([FromQuery] int index, int count)
        {
            if (index == 0 && count == 0)
            {
                return BadRequest();
            }
            if (count > 50)
            {
                return new JsonResult(new { message = "count must be lower than 50!" });
            }
            try
            {
                return new JsonResult(Users.GetRange(index, count).ToArray());
            }
            catch
            {
                Response.StatusCode = 404;
                return new JsonResult(new { error = 404, message = "no users in that range" });
            }

        }


        [Route("{id?}")]
        [HttpDelete]
        public IActionResult Delete([FromRoute] string id)
        {
            var tempUser = Users.FirstOrDefault(e => { return e.Id == id; });

            if (tempUser == null)
            {
                return new JsonResult(new { error = 404, description = $"Cant find user with {id} id" });
            }
            Users.Remove(tempUser);
            return StatusCode(200);
        }

    }
}
