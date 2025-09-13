using System.Collections.Generic;

namespace Moq.Contrib.InOrder.Exceptions;

public class UnexpectedMoqInvocationException(IEnumerable<string> unexpectedCalls) :
    MoqException("The following calls were not expected in the current setup:" +
                 $"{System.Environment.NewLine}{string.Join(System.Environment.NewLine, unexpectedCalls)}");