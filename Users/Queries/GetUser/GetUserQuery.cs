using MediatR;
using Social_Network_API.Entities;

namespace Social_Network_API.Users.Queries.GetUser
{
    public class GetUserQuery:IRequest<User>
    {

        public readonly string? Email;
        public readonly int? Id;
        public readonly HttpContext? Context;


        public GetUserQuery(int id)
        {
            Id = id;
        }
        public GetUserQuery(string email)
        {
            Email = email;
        }
        public GetUserQuery(HttpContext context)
        {
            Context = context;
        }

    }
}
