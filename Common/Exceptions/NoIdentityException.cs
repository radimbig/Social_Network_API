using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Common.Exceptions
{
    public class NoIdentityException:Exception,ICustomException
    {
        public NoIdentityException() : base("Token expired or user does not exist anymore") { }

        public string View => "Token expired or user does not exist anymore";

        public int StatusCode => StatusCodes.Status400BadRequest;
    }
}
