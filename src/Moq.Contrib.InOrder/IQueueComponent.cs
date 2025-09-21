using System;

namespace Moq.Contrib.InOrder
{
    public interface IQueueComponent
    {
        IQueueComponent? Parent { get; }

        void RegisterLoop(Action<IQueueComponent> setups, Times times);

        Call RegisterCall(string mockClassName, string callExpression, Times times);

        CallQueue GetRoot();
    }
}