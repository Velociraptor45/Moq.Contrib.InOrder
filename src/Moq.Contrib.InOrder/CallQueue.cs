using Moq.Contrib.InOrder.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("Moq.Contrib.InOrder.Tests")]

namespace Moq.Contrib.InOrder
{
    public class CallQueue : QueueComponentBase
    {
        private readonly Calls _unverifiedReceivedCalls = new Calls();
        private readonly List<Call> _receivedCalls = new List<Call>();
        public IReadOnlyCollection<Call> ReceivedCalls => _receivedCalls;

        public static CallQueue Create(Action<IQueueComponent> value, ILogger<CallQueue> logger = null)
        {
            Logger = logger;

            var queue = new CallQueue();
            RootInstance = queue;
            CurrentInstance = queue;

            Logger?.LogInformation("Expected Calls:");
            value(queue);

            Logger?.LogInformation("----------------------------");
            Logger?.LogInformation("Received Calls:");

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