using System;
using System.Collections.Generic;

namespace Moq.Contrib.InOrder
{
    public abstract class QueueComponenetBase : IQueueComponent
    {
        internal readonly List<IQueueItem> Items = new List<IQueueItem>();

        [ThreadStatic]
        public static CallQueue RootInstance;

        [ThreadStatic]
        internal static IQueueComponent CurrentInstance;

        public Call RegisterCall(string callExpression, Times times)
        {
            var call = new Call(callExpression, times);
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