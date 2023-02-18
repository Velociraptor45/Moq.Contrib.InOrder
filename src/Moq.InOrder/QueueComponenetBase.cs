using System;
using System.Collections.Generic;

namespace Moq.InOrder
{
    public abstract class QueueComponenetBase : IQueueComponent
    {
        internal readonly List<IQueueItem> Items = new List<IQueueItem>();
        public static CallQueue RootInstance { get; set; }
        internal static IQueueComponent CurrentInstance { get; set; }

        public Call RegisterCall(string callExpression, Func<Times> times)
        {
            var call = new Call(callExpression, times());
            Items.Add(call);
            return call;
        }

        public void RegisterLoop(Action<IQueueComponent> value)
        {
            RegisterLoop(value, Times.Once());
        }

        public void RegisterLoop(Action<IQueueComponent> value, Times times)
        {
            var loop = new Loop(times);
            Items.Add(loop);
            CurrentInstance = loop;

            value(loop);

            CurrentInstance = this;
        }
    }
}