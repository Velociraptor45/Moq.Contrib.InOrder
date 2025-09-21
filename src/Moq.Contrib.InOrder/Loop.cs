using Moq.Contrib.InOrder.Exceptions;
using System;
using System.Linq;

namespace Moq.Contrib.InOrder
{
    internal sealed class Loop : QueueComponentBase, IQueueItem
    {
        private readonly Times _times;

        public Loop(Times times, IQueueComponent parent)
        {
            _times = times;
            Parent = parent;
            
            if (parent is QueueComponentBase { LoggingIndentation: not null } componentBase)
                LoggingIndentation = componentBase.LoggingIndentation + "\t";
        }

        public string Expression
        {
            get
            {
                var expressions = Items.Select(x => x.Expression);
                return string.Join($",{Environment.NewLine}", expressions);
            }
        }

        internal override string? LoggingIndentation { get; }
        public override IQueueComponent? Parent { get; }
        
        public override CallQueue GetRoot()
        {
            return Parent!.GetRoot();
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
                    throw new MoqOrderViolatedException($"Expected loop{Environment.NewLine}{Expression}{Environment.NewLine}exactly {min} time(s) but received it {actualCount} time(s)");

                throw new MoqOrderViolatedException($"Expected loop{Environment.NewLine}{Expression}{Environment.NewLine}between {min} and {max} time(s) but received it {actualCount} time(s)");
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