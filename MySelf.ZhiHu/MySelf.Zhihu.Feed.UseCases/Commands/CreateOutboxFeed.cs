using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.UseCases.Commands
{
    public record CreateOutboxFeedCommand(int SenderId, int FeedId, FeedType FeedType) : ICommand<Result>;

    public class CreateOutboxFeedHandler(
        IMapper mapper,
        IRepository<Outbox> outbox) : ICommandHandler<CreateOutboxFeedCommand, Result>
    {
        public async Task<Result> Handle(CreateOutboxFeedCommand request, CancellationToken cancellationToken)
        {
            var sepc = new OutboxFeedByFeedIdSpec(request.FeedId, request.FeedType);
            if (await outbox.AnyAsync(sepc, cancellationToken))
                return Result.Success();

            var feed = mapper.Map<Outbox>(request);
            outbox.Add(feed);
            await outbox.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
