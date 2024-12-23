using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MySelf.Zhihu.Feed.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.UseCases.Consumers
{
    public class FeedCreatedConsumer(ISender sender, ILogger<FeedCreatedConsumer> logger) : IConsumer<FeedCreatedEvent>
    {
        public async Task Consume(ConsumeContext<FeedCreatedEvent> context)
        {
            logger.Log(LogLevel.Information, JsonSerializer.Serialize(context.Message));

            // 写发件箱（可选）
            await sender.Send(new CreateOutboxFeedCommand(
                context.Message.UserId,
                context.Message.FeedId,
                context.Message.FeedType));

            // 写收件箱
            await sender.Send(new CreateInboxFeedsCommand(
                context.Message.UserId,
                context.Message.FeedId,
                context.Message.FeedType));
        }
    }

}
