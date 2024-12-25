using MySelf.Zhihu.SharedKernel.Messaging;
using MySelf.Zhihu.SharedKernel.Result;
using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers.Queries
{
    [Authorize]
    public record GetUserInfoQuery(int UserId) : IQuery<Result<UserInfoDto>>;

    public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
    {
        public GetUserInfoQueryValidator()
        {
            RuleFor(command => command.UserId)
                .GreaterThan(0);
        }
    }

    public class GetUserInfoQueryHandler(IDataQueryService queryService)
        : IQueryHandler<GetUserInfoQuery, Result<UserInfoDto>>
    {
        public async Task<Result<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var queryable = queryService.AppUsers
                .Where(u => u.Id == request.UserId)
                .Select(u => new UserInfoDto
                {
                    Id = u.Id,
                    Nickname = u.Nickname,
                    Avatar = u.Avatar,
                    Bio = u.Bio,
                    FolloweesCount = u.Followees.Count,
                    FollowersCount = u.Followers.Count
                });

            var appUserInfo = await queryService.FirstOrDefaultAsync(queryable);

            return appUserInfo is null ? Result.NotFound() : Result.Success(appUserInfo);
        }
    }
}
