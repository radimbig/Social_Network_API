using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network_API.Users.Queries.GetUsers
{
    public class GetUsersQueryValidator:AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator()
        {
            RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count).LessThanOrEqualTo(30).GreaterThanOrEqualTo(1);
        }
    }
}
