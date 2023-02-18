using System.Collections.Generic;

namespace Moq.InOrder
{
    internal interface IQueueItem
    {
        void VerifyOrder(IList<Call> callQueue);

        bool StartsWith(Call call);
    }
}