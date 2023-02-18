using Moq.InOrder.Exceptions;
using System.Linq;

namespace Moq.InOrder
{
    public class Call : IQueueItem
    {
        private readonly Times _times;

        public Call(string expression, Times times)
        {
            Expression = expression;
            _times = times;
        }

        public string Expression { get; }

        public void VerifyOrder(Calls callQueue)
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
                callQueue.RemoveFirst();
            }

            if (actualCount < min || max < actualCount)
            {
                if (min == max)
                    throw new MoqOrderViolatedException($"Expected {Expression} exactly {min} times but received it {actualCount} time(s)");

                throw new MoqOrderViolatedException($"Expected {Expression} between {min} and {max} times but received it {actualCount} time(s)");
            }
        }

        public bool StartsWith(Call call)
        {
            return call == this;
        }
    }
}