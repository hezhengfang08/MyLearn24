using MySelf.Zhihu.Feed.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.UseCases.Commands
{
    public record CreateInboxFeedsCommand(int SenderId, int FeedId, FeedType FeedType) : ICommand<Result>;

    public class CreateInboxFeedsHandler(
        IMapper mapper,
        IFollowUserQueryService followUserQueryService,
        IRepository<Inbox> inbox) : ICommandHandler<CreateInboxFeedsCommand, Result>
    {
        public async Task<Result> Handle(CreateInboxFeedsCommand request, CancellationToken cancellationToken)
        {
            var followerIds = await followUserQueryService.GetFollowerIdsAsync(request.SenderId);

            if (followerIds.Length == 0) return Result.Success();

            foreach (var id in followerIds)
            {
                var feed = mapper.Map<Inbox>(request);
                feed.UserId = id;
                inbox.Add(feed);
            }

            await inbox.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
