using System.Collections.Generic;

namespace Moq.InOrder
{
    internal interface IQueueItem
    {
        string Expression { get; }

        void VerifyOrder(IList<Call> callQueue);

        bool StartsWith(Call call);
    }
}