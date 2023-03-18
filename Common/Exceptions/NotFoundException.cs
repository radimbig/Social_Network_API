using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(object entity, object key)
            : base($"Entity \"{nameof(entity)}\" ({key}) not found.") { }
    }
}
