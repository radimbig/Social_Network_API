using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Common.Exceptions
{
    public class NotFoundException : Exception, ICustomException
    {
        public string View { get; }

        public NotFoundException(string type, object key)
            : base($"{type} with id {key} not founded")
        {
            View = $"{type} with id {key} not found";
        }
        public NotFoundException(string customText):base(customText) 
        {
            View = customText;
        }
        public int StatusCode => StatusCodes.Status404NotFound;
    }
}
