using System;

namespace Moq.Contrib.InOrder.Exceptions
{
    public abstract class MoqException(string message) : Exception(message);
}