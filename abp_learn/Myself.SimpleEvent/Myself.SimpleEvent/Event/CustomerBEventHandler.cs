﻿using Myself.SimpleEvent.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SimpleEvent.Client.Event
{
    public class CustomerBEventHandler : IEventHandler<SendedEvent>
    {
        public void Handle(SendedEvent @event)
        {
            Console.WriteLine($"客户B收到{@event.Name}-{@event.Description} 通知【时间:{@event.EventAt}】【Id:{@event.EventId}】！");
        }
    }
}
