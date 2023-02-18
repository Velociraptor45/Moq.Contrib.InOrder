using System;
using System.Linq.Expressions;

namespace Moq.InOrder.Extensions
{
    public static class MoqExtensions
    {
        public static Language.Flow.ISetup<T> SetupInOrder<T>(this Mock<T> mock, Expression<Action<T>> expression)
            where T : class
        {
            return mock.SetupInOrder(expression, Times.Once);
        }

        public static Language.Flow.ISetup<T> SetupInOrder<T>(this Mock<T> mock, Expression<Action<T>> expression,
            Func<Times> times) where T : class
        {
            var setup = mock.Setup(expression);
            var call = Queue.CurrentInstance.RegisterCall(setup.ToString(), times);
            setup.Callback(() => Queue.RootInstance.ReceiveCall(call));
            return setup;
        }

        public static Language.Flow.ISetup<T, TResult> SetupInOrder<T, TResult>(this Mock<T> mock,
            Expression<Func<T, TResult>> expression) where T : class
        {
            return mock.Setup(expression);
        }
    }
}