using Myself.SimpleEvent.Bus;
using Myself.SimpleEvent.Client.Event;
using System;

//应该可以都次的
var sendEvent = new SendedEvent("优惠", "测试发布消息描述");
var eventBus = new SimpleEventBus<SendedEvent>();


var customerA = new CustomerAEventHandler();
var customerB = new CustomerBEventHandler();    
 
var eventHandlers = new IEventHandler<SendedEvent>[] { customerA, customerB };
//订阅
foreach (var eventHander in eventHandlers)
{
    eventBus.SubsScribe(eventHander);
}
Console.WriteLine($"商店发送{sendEvent.Name}通知!");
eventBus.Publish(sendEvent);
//某个取消订阅
eventBus.UnSubsScribe(customerA);
Console.WriteLine($"\n商店发送第二次{sendEvent.Name}通知!");
eventBus.Publish(sendEvent);
Console.ReadKey();
