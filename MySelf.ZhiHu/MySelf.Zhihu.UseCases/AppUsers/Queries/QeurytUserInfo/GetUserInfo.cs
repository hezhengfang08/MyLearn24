using MySelf.Zhihu.SharedKernel.Messaging;
using MySelf.Zhihu.SharedKernel.Result;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers.Queries.QeurytUserInfo
{
    public record GetUserInfoQuery(int Id): IQuery<Result<UserInfoDto>>;
    public class GetUserInfoQueryHandler(IDataQueryService queryService) : IQueryHandler<GetUserInfoQuery, Result<UserInfoDto>>

    {
        public async Task<Result<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var queryAlbe = queryService.AppUsers.Where(u => u.Id == request.Id)
                  .Select(u => new UserInfoDto
                  {
                      Id = u.Id,
                      Nickname = u.Nickname,
                      Avatar = u.Avatar,
                      Bio = u.Bio,
                      FolloweesCount = u.Followees.Count,
                      FollowersCount = u.Followers.Count
                  });
            var appUserInfo = await queryService.FirstOrDefaultAsync(queryAlbe);
            return appUserInfo is null ? Result.NotFound() : Result.Success(appUserInfo);  
        }
    }
}
