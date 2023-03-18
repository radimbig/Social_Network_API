using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Social_Network_API.Entities;

namespace Social_Network_API.Users.Queries.GetUser
{
    public class UserVm
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null;

        public int Age { get; set; } 

        
    }
}
