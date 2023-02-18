﻿using System;

namespace Moq.InOrder
{
    public interface IQueueComponent
    {
        void RegisterLoop(Action<IQueueComponent> value);

        void RegisterLoop(Action<IQueueComponent> value, Times times);

        Call RegisterCall(string callExpression, Func<Times> times);
    }
}