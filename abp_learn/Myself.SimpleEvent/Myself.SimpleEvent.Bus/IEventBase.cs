namespace Myself.SimpleEvent.Bus
{
    public interface IEventBase
    {
        /// <summary>
        /// 事件发生时间
        /// </summary>
        DateTime EventAt { get; }
        /// <summary>
        /// 事件Id
        /// </summary>
        string EventId {  get; }
    }
}
