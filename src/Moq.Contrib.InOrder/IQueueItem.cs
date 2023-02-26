namespace Moq.Contrib.InOrder
{
    internal interface IQueueItem
    {
        string Expression { get; }

        void VerifyOrder(Calls callQueue);

        bool StartsWith(Call call);
    }
}