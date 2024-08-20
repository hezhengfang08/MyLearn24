using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SimpleEvent.Bus
{
    public interface IEventBus<TEvent> where TEvent : IEventBase
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event">事件</param>
        void Publish(TEvent @event);
        /// <summary>
        ///  订阅，添加事件处理实例
        /// </summary>
        /// <param name="eventHandler">事件处理</param>
        void SubsScribe(IEventHandler<TEvent> eventHandler);
        /// <summary>
        ///  取消订阅，删除事件处理实例
        /// </summary>
        /// <param name="eventHandler"></param>
        void UnSubsScribe(IEventHandler<TEvent> eventHandler);
    }
}
