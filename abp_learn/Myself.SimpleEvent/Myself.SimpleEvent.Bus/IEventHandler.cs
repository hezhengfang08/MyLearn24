using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SimpleEvent.Bus
{
    public interface IEventHandler<TEvent> where TEvent:IEventBase
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="event">事件源</param>
        void Handle(TEvent @event);
    }
}
