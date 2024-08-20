using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SimpleEvent.Bus
{
    /// <summary>
    /// 事件总线实现类
    /// </summary>
    /// <typeparam name="TEvent">事件源</typeparam>
    public class SimpleEventBus<TEvent> : IEventBus<TEvent> where TEvent : IEventBase
    {
        // 将数据放在内存中的实现，使用了 ConcurrentDictionary 以及 HashSet 来尽可能的保证高效
        // 事件源与事件处理的映射字典
        private readonly ConcurrentDictionary<string,HashSet<IEventHandler<TEvent>>> _eventHandlers = new ConcurrentDictionary<string, HashSet<IEventHandler<TEvent>>>();
        /// <summary>
        /// 发布事件，通过事件源找到对应的事件处理类，并执行它
        /// </summary>
        /// <param name="event">事件</param>
        /// <exception cref="NotImplementedException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2200:Rethrow to preserve stack details", Justification = "<Pending>")]
        public void Publish(TEvent @event)
        {
            var eventTypeName = typeof(TEvent).FullName;
            if(string.IsNullOrEmpty(eventTypeName))
            {
                throw new ArgumentNullException(nameof(eventTypeName));
            }
            if (_eventHandlers.ContainsKey(eventTypeName))
            {
                //订阅事件集合
                var handlers = _eventHandlers[eventTypeName];
                try
                {
                    foreach (var handler in handlers)
                    {
                        MethodInfo methodInfo = handler.GetType().GetMethod("Handle");
                        if (methodInfo != null)
                        {
                            //发布事件
                            methodInfo.Invoke(handler, new object[] { @event });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 实现订阅，往字典表中添加事件处理实例
        /// </summary>
        /// <param name="eventHandler">事件处理</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SubsScribe(IEventHandler<TEvent> eventHandler)
        {
            var eventTypeName = typeof(TEvent).FullName;
            if (string.IsNullOrEmpty(eventTypeName))
            {
                throw new ArgumentNullException(nameof(eventTypeName));
            }
            if (_eventHandlers.ContainsKey(eventTypeName))
            {
                var handlers = _eventHandlers[eventTypeName];
                //添加订阅项
                handlers.Add(eventHandler);
            }
            else
            {
                //添加订阅
                var hashSet = new HashSet<IEventHandler<TEvent>>();
                hashSet.Add(eventHandler);
                _eventHandlers.TryAdd(eventTypeName, hashSet);
            }

        }
        /// <summary>
        ///  实现取消订阅，往字典表中删除事件处理实例
        /// </summary>
        /// <param name="eventHandler">事件处理</param>
        /// <exception cref="NotImplementedException"></exception>
        public void UnSubsScribe(IEventHandler<TEvent> eventHandler)
        {
            var eventTypeName = typeof(TEvent).FullName;
            if (string.IsNullOrEmpty(eventTypeName))
            {
                throw new ArgumentNullException(nameof(eventTypeName));
            }
            if (_eventHandlers.ContainsKey(eventTypeName))
            {
                var handlers = _eventHandlers[eventTypeName];
                if (handlers != null && handlers.Any(s => s.GetType() == eventHandler.GetType()))
                {
                    var handerToRemove = handlers.Single(s => s.GetType() == eventHandler.GetType());
                    handlers.Remove(handerToRemove);
                }
            }
        }
    }
}
