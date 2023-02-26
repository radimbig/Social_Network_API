using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Social_Network_API.Enums;

namespace Social_Network_API.Converters
{
    public class UserRoleConverter : ValueConverter<UserRole, byte>
    {
        public UserRoleConverter() : base(
            v => (byte)v,
            v => (UserRole)v)
        { }
    }
}
