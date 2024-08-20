using Myself.SimpleEvent.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SimpleEvent.Client.Event
{
    public class SendedEvent : IEventBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; } 
        public SendedEvent(string name,string description)
        {
            Name = name;
            Description = description;
        }
        public DateTime EventAt =>  DateTime.Now;

        public string EventId => Guid.NewGuid().ToString();
    }
}
