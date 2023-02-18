using Moq.InOrder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moq.InOrder
{
    internal class Loop : QueueComponenetBase, IQueueItem
    {
        private readonly Times _times;

        public Loop(Times times)
        {
            _times = times;
        }

        public void VerifyOrder(IList<Call> callQueue)
        {
            if (!Items.Any())
                throw new InvalidOperationException("Loop does not contain any setups");

            var (min, max) = _times;
            var actualCount = 0;

            while (true)
            {
                if (!callQueue.Any()
                    || !Items.First().StartsWith(callQueue.First()))
                {
                    break;
                }

                foreach (var item in Items)
                {
                    item.VerifyOrder(callQueue);
                }

                actualCount++;
            }

            if (actualCount < min || max < actualCount)
            {
                if (min == max)
                    throw new MoqOrderViolatedException($"Expected loop exactly {min} times but received it {actualCount} times");

                throw new MoqOrderViolatedException($"Expected loop between {min} and {max} times but received it {actualCount} times");
            }
        }

        public bool StartsWith(Call call)
        {
            if (!Items.Any())
                throw new InvalidOperationException("Loop does not contain any setups");

            return Items.First().StartsWith(call);
        }
    }
}