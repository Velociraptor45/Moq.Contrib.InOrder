using System;

namespace Moq.Contrib.InOrder
{
    public interface IQueueComponent
    {
        void RegisterLoop(Action<IQueueComponent> value);

        void RegisterLoop(Action<IQueueComponent> value, Times times);

        Call RegisterCall(string callExpression, Times times);
    }
}