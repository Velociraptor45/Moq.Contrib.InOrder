using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Moq.Contrib.InOrder
{
    public abstract class QueueComponentBase : IQueueComponent
    {
        internal readonly List<IQueueItem> Items = new();
        
        internal abstract string? LoggingIndentation { get; }
        public abstract IQueueComponent? Parent { get; }

        public Call RegisterCall(string mockClassName, string callExpression, Times times)
        {
            var call = new Call(mockClassName, callExpression, times);
            Items.Add(call);

            Log($"{GetTimesLogging(times)}{callExpression}");

            return call;
        }

        public abstract CallQueue GetRoot();

        public void RegisterLoop(Action<IQueueComponent> setups, Times times)
        {
            var loop = new Loop(times, this);
            Items.Add(loop);

            Log($"Loop: {GetTimesLogging(times)}");

            setups(loop);
        }

        private static string GetTimesLogging(Times times)
        {
            var (min, max) = times;

            if (min == max)
            {
                if (min == 1)
                    return string.Empty;

                return $"({min} times) ";
            }

            if (max == int.MaxValue)
                return $"(>={min} times) ";

            return $"({min} - {max} times) ";
        }


        private void Log(string msg)
        {
            var logger = GetRoot().Logger;
            if (logger is null)
                return;
            
            logger.LogInformation($"{LoggingIndentation}{msg}");
        }
    }
}