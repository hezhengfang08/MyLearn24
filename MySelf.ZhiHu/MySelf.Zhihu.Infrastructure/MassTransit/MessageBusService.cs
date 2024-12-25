using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.MassTransit
{
    public class MessageBusService(IPublishEndpoint publishEndpoint) : IMessageBusService
    {
        public async Task PushishAsync<TMessege>(TMessege message) where TMessege : class
        {
            await publishEndpoint.Publish(message);
        }
    }
}
