using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Moq.InOrder.Tests")]

namespace Moq.InOrder
{
    public class CallQueue : QueueComponenetBase
    {
        private readonly IList<Call> _receivedCalls = new List<Call>();

        public static CallQueue Create(Action<IQueueComponent> value)
        {
            var queue = new CallQueue();
            RootInstance = queue;
            CurrentInstance = queue;
            value(queue);

            return queue;
        }

        internal void ReceiveCall(Call call)
        {
            _receivedCalls.Add(call);
        }

        public void VerifyOrder()
        {
            foreach (var item in Items)
            {
                item.VerifyOrder(_receivedCalls);
            }
        }
    }
}