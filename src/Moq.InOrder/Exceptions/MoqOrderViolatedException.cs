using System;

namespace Moq.InOrder.Exceptions
{
    public class MoqOrderViolatedException : Exception
    {
        public MoqOrderViolatedException(string message) : base(message)
        {
        }
    }
}