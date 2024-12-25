using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers.Queries
{
    public record GetFollowerUsersQuery(int UserId) : IQuery<IResult>;
    public class GetFollowerUsersQueryValidator : AbstractValidator<GetFollowerUsersQuery>
    {
        public GetFollowerUsersQueryValidator()
        {
            RuleFor(command => command.UserId)
                .GreaterThan(0);
        }
    }
}
