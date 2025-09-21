using System;

namespace Moq.Contrib.InOrder.Exceptions
{
    public class MoqOrderViolatedException : MoqException
    {
        public MoqOrderViolatedException(string message) : base(message)
        {
        }
    }
}