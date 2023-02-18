using Moq.InOrder.Exceptions;
using System;
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

        public string Expression
        {
            get
            {
                var expressions = Items.Select(x => x.Expression);
                return string.Join($",{Environment.NewLine}", expressions);
            }
        }

        public void VerifyOrder(Calls callQueue)
        {
            if (!Items.Any())
                throw new InvalidOperationException("Loop does not contain any setups");
            if (Items.Count == 1)
                throw new InvalidOperationException(
                    "Loops with only one call are currently not supported. Please use the 'times' argument on '.SetupInOrder()' instead");

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
                    if (!callQueue.Any())
                    {
                        throw new MoqOrderViolatedException(
                            $"Loop{Environment.NewLine}{Expression}{Environment.NewLine}is missing {item.Expression} after {actualCount} complete run(s)");
                    }

                    item.VerifyOrder(callQueue);
                }

                actualCount++;
            }

            if (actualCount < min || max < actualCount)
            {
                if (min == max)
                    throw new MoqOrderViolatedException($"Expected loop{Environment.NewLine}{Expression}{Environment.NewLine}exactly {min} times but received it {actualCount} time(s)");

                throw new MoqOrderViolatedException($"Expected loop{Environment.NewLine}{Expression}{Environment.NewLine}between {min} and {max} times but received it {actualCount} time(s)");
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