using MySelf.Zhihu.Feed.UseCases.Common.Interfaces;
using MySelf.Zhihu.SharedKernel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.UseCases.Queries
{
    public record GetInboxFeedsQuery(int Userid, Pagination Pagination) : IQuery<Result<PagedList<FeedDto>>>;

    public class GetInboxFeedsQueryHandler(IDataQueryService dataQueryService) : IQueryHandler<GetInboxFeedsQuery, Result<PagedList<FeedDto>>>
    {
        public async Task<Result<PagedList<FeedDto>>> Handle(GetInboxFeedsQuery request, CancellationToken cancellationToken)
        {
            var queryable = from feed in dataQueryService.Inbox
                            where feed.UserId == request.Userid
                            orderby feed.ReceivedAt descending
                            select new FeedDto
                            {
                                FeedId = feed.FeedId,
                                FeedType = feed.FeedType,
                                SenderId = feed.SenderId
                            };

            var result = await dataQueryService.ToPageListAsync(queryable, request.Pagination);

            return result == null ? Result.NotFound() : Result.Success(result);
        }
    }

}
