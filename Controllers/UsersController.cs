using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Entities;

using Microsoft.AspNetCore.Http;
using Social_Network_API.Models;
using Social_Network_API.Database;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Social_Network_API.Enums;
using Social_Network_API.Common.Exceptions;

namespace Social_Network_API.Controllers
{


    
    
    [Route("user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDBContext _context;
        
        

        public UsersController(MyDBContext context)
        {
            _context = context;
            
        }

        private User? GetCurrentUser()
        {

            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if (identity != null)
            {

                var userClaims = identity.Claims;
                int idToSearch = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                

                if(!_context.Users.Any(x => x.Id == idToSearch))
                {
                    return null;
                }
                User? temp = _context.Users.Where(x => x.Id == idToSearch).ToArray().First();
                if(temp != null)
                {
                    return temp;
                }
                
            }
            return null;
        }
        [Route("me")]
        [Authorize]
        public IActionResult GetMe()
        {

            User? response = GetCurrentUser();
            if(response!= null)
            {
                return new JsonResult(response);
            }
            return new StatusCodeResult(500);
            
        }


        [AllowAnonymous]
        [Route("{id?}")]
        [HttpGet]
        public IActionResult Get([FromRoute] int id)
        {
            if (!_context.Users.Any(e => e.Id == id))
            {
                HttpContext.Response.StatusCode = 404;
                return new JsonResult(new { description = $"Cant find user with {id} id" });
            }
            var tempUser = _context.Users.Where(e =>e.Id == id);
            
            return new JsonResult(tempUser.ToArray().First());
        }
        [AllowAnonymous]
        [Route("count")]
        [HttpGet]
        public IActionResult Count()
        {
            throw new DBException();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetList([FromQuery] int index, int count)
        {
            if(index <0 || count < 0)
            {
                return BadRequest();
            }
            if (index == 0 && count == 0)
            {
                return BadRequest();
            }
            if (count > 50)
            {
                return new JsonResult(new { description = "count must be lower than 50!" });
            }
            try
            {
                return new JsonResult(_context.Users.OrderByDescending(e=>e.Id).Skip(index).Take(count).ToArray()) ;
            }
            catch
            {
                Response.StatusCode = 404;
                return new JsonResult(new { description = "no users in that range" });
            }

        }

        [Authorize]
        
        [Route("{id?}")]
        [HttpDelete]
        public IActionResult Delete([FromRoute] int id)
        {
            if(GetCurrentUser().Role != UserRole.Admin)
            {
                return StatusCode(403);
            }
            if (!_context.Users.Any(e => e.Id == id))
            {
                HttpContext.Response.StatusCode = 409;
                return new JsonResult(new { description = $"no user with id {id}"});
            }
            _context.Users.Remove(_context.Users.Where(e=>e.Id == id).ToArray().First());
            _context.SaveChanges();
            return StatusCode(200);
        }

    }
}



/*
             
            
            
 */