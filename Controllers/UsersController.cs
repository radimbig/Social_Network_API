﻿using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Entities;

using Microsoft.AspNetCore.Http;
using Social_Network_API.Models;
using Social_Network_API.Database;

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
        [Route("count")]
        [HttpGet]
        public IActionResult Count()
        {
            return new JsonResult(new { TotalUsersCount = _context.Users.Count() });
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


        [Route("{id?}")]
        [HttpDelete]
        public IActionResult Delete([FromRoute] int id)
        {

            if (!_context.Users.Any(e => e.Id == id))
            {
                HttpContext.Response.StatusCode = 409;
                return new JsonResult(new { description = $"no user with id{id}"});
            }
            _context.Users.Remove(_context.Users.Where(e=>e.Id == id).ToArray().First());
            _context.SaveChanges();
            return StatusCode(200);
        }

    }
}



/*
             
            
            
 */