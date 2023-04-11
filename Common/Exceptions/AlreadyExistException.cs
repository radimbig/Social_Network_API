using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Common.Exceptions
{
    public class AlreadyExistException : Exception, ICustomException
    {
        public object exceptionCause;
        public AlreadyExistException(object entity):base($"This {entity} is already exists") {
        exceptionCause = entity;
        }

        public string View => $"";

        public int StatusCode => throw new NotImplementedException();
    }
}
