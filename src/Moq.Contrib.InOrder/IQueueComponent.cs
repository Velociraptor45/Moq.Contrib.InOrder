using System;

namespace Moq.Contrib.InOrder
{
    public interface IQueueComponent
    {
        void RegisterLoop(Action<IQueueComponent> setups);

        void RegisterLoop(Action<IQueueComponent> setups, Times times);

        Call RegisterCall(string callExpression, Times times);
    }
}