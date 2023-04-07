using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Common.Exceptions
{
    public class DBException:Exception, ICustomException
    {
        public DBException() : base("Problems with database") { }
        public string View => "Internal server error";
        public int StatusCode => 500;
    }
}
