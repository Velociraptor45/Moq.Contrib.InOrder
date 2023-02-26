using System;

namespace Moq.Contrib.InOrder.Exceptions
{
    public class MoqOrderViolatedException : Exception
    {
        public MoqOrderViolatedException(string message) : base(message)
        {
        }
    }
}