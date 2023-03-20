using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Common.Exceptions
{
    public class NoIdentityException:Exception
    {
        public NoIdentityException() : base("No Identity in this context") { }
    }
}
