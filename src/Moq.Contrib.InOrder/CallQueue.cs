using Moq.Contrib.InOrder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("Moq.Contrib.InOrder.Tests")]

namespace Moq.Contrib.InOrder
{
    public sealed class CallQueue : QueueComponentBase
    {
        private readonly Calls _unverifiedReceivedCalls = new();
        private readonly List<Call> _receivedCalls = new();
        public IReadOnlyCollection<Call> ReceivedCalls => _receivedCalls;

        internal override string? LoggingIndentation { get; }
        public override IQueueComponent? Parent => null;
        public ILogger<CallQueue>? Logger { get; }

        private CallQueue(ILogger<CallQueue>? logger)
        {
            Logger = logger;
            if(logger is not null)
                LoggingIndentation = "\t";
        }
        
        public override CallQueue GetRoot()
        {
            return this;
        }

        public static CallQueue Create(Action<IQueueComponent> value, ILogger<CallQueue>? logger = null)
        {
            var queue = new CallQueue(logger);

            queue.Logger?.LogInformation("Expected Calls:");
            value(queue);

            queue.Logger?.LogInformation("----------------------------");
            queue.Logger?.LogInformation("Received Calls:");

            return queue;
        }

        internal void ReceiveCall(Call call)
        {
            _unverifiedReceivedCalls.Add(call);
            _receivedCalls.Add(call);
            Logger?.LogInformation($"\t{call.Expression}");
        }

        /// <summary>
        /// Verifies whether the received calls match exactly the order of the expected calls
        /// </summary>
        /// <exception cref="MoqOrderViolatedException">Thrown when received and expected call order is not equivalent</exception>
        public void VerifyOrder()
        {
            foreach (var item in Items)
            {
                item.VerifyOrder(_unverifiedReceivedCalls);
            }

            if (_unverifiedReceivedCalls.Any())
                throw new MoqOrderViolatedException(
                    $"All setups satisfied but the following calls are remaining and missing a corresponding setup:{Environment.NewLine}{_unverifiedReceivedCalls.Expressions}");
        }
    }
}