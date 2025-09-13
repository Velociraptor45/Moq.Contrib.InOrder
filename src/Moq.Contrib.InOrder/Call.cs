using System.ComponentModel;
using Moq.Contrib.InOrder.Exceptions;
using System.Diagnostics;
using System.Linq;

namespace Moq.Contrib.InOrder
{
    [DebuggerDisplay("{Expression}")]
    public record Call(string MockClassName, string Expression, Times Times) : IQueueItem
    {
        public void VerifyOrder(Calls callQueue)
        {
            var (min, max) = Times;
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
                    throw new MoqOrderViolatedException($"Expected {Expression} exactly {min} time(s) but received it {actualCount} time(s)");

                throw new MoqOrderViolatedException($"Expected {Expression} between {min} and {max} time(s) but received it {actualCount} time(s)");
            }
        }

        public bool StartsWith(Call call)
        {
            return call == this;
        }
    }
}

// this fixes an "IsExternalInit is not defined" error in the compiler
namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class IsExternalInit{}
}