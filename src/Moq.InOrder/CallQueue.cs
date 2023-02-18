using Moq.InOrder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moq.InOrder
{
    public static class Queue
    {
        public static CallQueue RootInstance { get; set; }
        public static IQueueComponent CurrentInstance { get; set; }
    }

    public class Call : IQueueItem
    {
        private readonly Times _times;

        public Call(string expression, Times times)
        {
            Expression = expression;
            _times = times;
        }

        public string Expression { get; }

        public void VerifyOrder(IList<Call> callQueue)
        {
            var (min, max) = _times;
            var actualCount = 0;

            while (callQueue.Any())
            {
                var call = callQueue.First();
                if (call != this)
                {
                    break;
                }

                actualCount++;
                callQueue.RemoveAt(0);
            }

            if (actualCount < min || max < actualCount)
            {
                if (min == max)
                    throw new MoqOrderViolatedException($"Expected {Expression} exactly {min} times but received it {actualCount} times");

                throw new MoqOrderViolatedException($"Expected {Expression} between {min} and {max} times but received it {actualCount} times");
            }
        }

        public bool IsSatisfiedBy(Call call)
        {
            return call == this;
        }
    }

    public class Loop : IQueueComponent, IQueueItem
    {
        private readonly List<IQueueItem> _items = new List<IQueueItem>();
        private readonly Times _times;

        public Loop(Times times)
        {
            _times = times;
        }

        public void RegisterLoop(Action<IQueueComponent> value, Times times)
        {
            var loop = new Loop(times);
            _items.Add(loop);
            Queue.CurrentInstance = loop;

            value(loop);

            Queue.CurrentInstance = this;
        }

        public void RegisterLoop(Action<IQueueComponent> value)
        {
            RegisterLoop(value, Times.Once());
        }

        public Call RegisterCall(string callExpression, Func<Times> times)
        {
            var call = new Call(callExpression, times());
            _items.Add(call);
            return call;
        }

        public void VerifyOrder(IList<Call> callQueue)
        {
            if (!_items.Any())
                throw new InvalidOperationException("Loop does not contain any setups");

            var (min, max) = _times;
            var actualCount = 0;

            while (true)
            {
                if (!callQueue.Any()
                    || !_items.First().IsSatisfiedBy(callQueue.First()))
                {
                    break;
                }

                foreach (var item in _items)
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

        public bool IsSatisfiedBy(Call call)
        {
            if (!_items.Any())
                throw new InvalidOperationException("Loop does not contain any setups");

            return _items.First().IsSatisfiedBy(call);
        }
    }

    public class CallQueue : IQueueComponent
    {
        private readonly List<IQueueItem> _items = new List<IQueueItem>();
        private readonly IList<Call> _receivedCalls = new List<Call>();

        public static CallQueue Create(Action<IQueueComponent> value)
        {
            var queue = new CallQueue();
            Queue.RootInstance = queue;
            Queue.CurrentInstance = queue;
            value(queue);

            return queue;
        }

        public void ReceiveCall(Call call)
        {
            _receivedCalls.Add(call);
        }

        public Call RegisterCall(string callExpression, Func<Times> times)
        {
            var call = new Call(callExpression, times());
            _items.Add(call);
            return call;
        }

        public void RegisterLoop(Action<IQueueComponent> value, Times times)
        {
            var loop = new Loop(times);
            _items.Add(loop);
            Queue.CurrentInstance = loop;

            value(loop);

            Queue.CurrentInstance = this;
        }

        public void RegisterLoop(Action<IQueueComponent> value)
        {
            RegisterLoop(value, Times.Once());
        }

        public void VerifyOrder()
        {
            foreach (var item in _items)
            {
                item.VerifyOrder(_receivedCalls);
            }
        }
    }

    public interface IQueueItem
    {
        void VerifyOrder(IList<Call> callQueue);

        bool IsSatisfiedBy(Call call);
    }

    public interface IQueueComponent
    {
        void RegisterLoop(Action<IQueueComponent> value);

        void RegisterLoop(Action<IQueueComponent> value, Times times);

        Call RegisterCall(string callExpression, Func<Times> times);
    }
}