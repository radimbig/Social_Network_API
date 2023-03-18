using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Common.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(object entity):base($"This {entity} is already exists") { }
        
    }
}
