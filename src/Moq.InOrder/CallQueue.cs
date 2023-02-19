using Moq.InOrder.Exceptions;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Moq.InOrder.Tests")]

namespace Moq.InOrder
{
    public class CallQueue : QueueComponenetBase
    {
        private readonly Calls _receivedCalls = new Calls();

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

            if (_receivedCalls.Any())
                throw new MoqOrderViolatedException(
                    $"All setups satisfied but the following calls are remaining and missing a corresponding setup:{Environment.NewLine}{_receivedCalls.Expressions}");
        }
    }
}