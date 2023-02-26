using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Moq.Contrib.InOrder
{
    public abstract class QueueComponentBase : IQueueComponent
    {
        internal readonly List<IQueueItem> Items = new List<IQueueItem>();

        [ThreadStatic]
        public static CallQueue RootInstance;

        [ThreadStatic]
        internal static IQueueComponent CurrentInstance;

        [ThreadStatic]
        internal static ILogger<CallQueue> Logger;

        [ThreadStatic]
        private static string _loggingIndentation;

        private static string LoggingIndentation
        {
            get => _loggingIndentation ?? (_loggingIndentation = "\t");
            set => _loggingIndentation = value;
        }

        public Call RegisterCall(string callExpression, Times times)
        {
            var call = new Call(callExpression, times);
            Items.Add(call);

            Logger?.LogInformation($"{LoggingIndentation}{GetTimesLogging(times)}{callExpression}");

            return call;
        }

        public void RegisterLoop(Action<IQueueComponent> setups)
        {
            RegisterLoop(setups, Times.Once());
        }

        public void RegisterLoop(Action<IQueueComponent> setups, Times times)
        {
            var loop = new Loop(times);
            Items.Add(loop);
            CurrentInstance = loop;

            if (Logger != null)
            {
                Logger.LogInformation($"{LoggingIndentation}Loop: {GetTimesLogging(times)}");
                LoggingIndentation = $"{LoggingIndentation}\t";
            }

            setups(loop);

            if (Logger != null)
                LoggingIndentation = LoggingIndentation.Remove(LoggingIndentation.Length - 1, 1);

            CurrentInstance = this;
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
                return $"(>{min} times) ";

            return $"({min} - {max} times) ";
        }
    }
}