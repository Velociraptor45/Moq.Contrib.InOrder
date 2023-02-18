using System;
using System.Linq.Expressions;

namespace Moq.InOrder.Extensions
{
    public static class MoqExtensions
    {
        public static Language.Flow.ISetup<T> SetupInOrder<T>(this Mock<T> mock, Expression<Action<T>> expression)
            where T : class
        {
            return mock.SetupInOrder(expression, Times.Once());
        }

        public static Language.Flow.ISetup<T> SetupInOrder<T>(this Mock<T> mock, Expression<Action<T>> expression,
            Times times) where T : class
        {
            var setup = mock.Setup(expression);
            var call = QueueComponenetBase.CurrentInstance.RegisterCall(setup.ToString(), times);
            setup.Callback(() => QueueComponenetBase.RootInstance.ReceiveCall(call));
            return setup;
        }

        public static Language.Flow.ISetup<T, TResult> SetupInOrder<T, TResult>(this Mock<T> mock,
            Expression<Func<T, TResult>> expression) where T : class
        {
            return mock.SetupInOrder(expression, Times.Once());
        }

        public static Language.Flow.ISetup<T, TResult> SetupInOrder<T, TResult>(this Mock<T> mock,
            Expression<Func<T, TResult>> expression, Times times) where T : class
        {
            var setup = mock.Setup(expression);
            var call = QueueComponenetBase.CurrentInstance.RegisterCall(setup.ToString(), times);
            setup.Callback(() => QueueComponenetBase.RootInstance.ReceiveCall(call));
            return setup;
        }

        public static Language.Flow.ISetupGetter<T, TProperty> SetupGetInOrder<T, TProperty>(this Mock<T> mock,
            Expression<Func<T, TProperty>> expression) where T : class
        {
            return mock.SetupGetInOrder(expression, Times.Once());
        }

        public static Language.Flow.ISetupGetter<T, TProperty> SetupGetInOrder<T, TProperty>(this Mock<T> mock,
            Expression<Func<T, TProperty>> expression, Times times) where T : class
        {
            var setup = mock.SetupGet(expression);
            var call = QueueComponenetBase.CurrentInstance.RegisterCall(setup.ToString(), times);
            setup.Callback(() => QueueComponenetBase.RootInstance.ReceiveCall(call));
            return setup;
        }

        public static Language.Flow.ISetupSetter<T, TProperty> SetupSetInOrder<T, TProperty>(this Mock<T> mock,
            Action<T> expression) where T : class
        {
            return mock.SetupSetInOrder<T, TProperty>(expression, Times.Once());
        }

        public static Language.Flow.ISetupSetter<T, TProperty> SetupSetInOrder<T, TProperty>(this Mock<T> mock,
            Action<T> expression, Times times) where T : class
        {
            var setup = mock.SetupSet<TProperty>(expression);
            var call = QueueComponenetBase.CurrentInstance.RegisterCall(setup.ToString(), times);
            setup.Callback(p => QueueComponenetBase.RootInstance.ReceiveCall(call));
            return setup;
        }

        public static Language.Flow.ISetup<T> SetupSetInOrder<T>(this Mock<T> mock,
            Action<T> expression) where T : class
        {
            return mock.SetupSetInOrder(expression, Times.Once());
        }

        public static Language.Flow.ISetup<T> SetupSetInOrder<T>(this Mock<T> mock,
            Action<T> expression, Times times) where T : class
        {
            var setup = mock.SetupSet(expression);
            var call = QueueComponenetBase.CurrentInstance.RegisterCall(setup.ToString(), times);
            setup.Callback(() => QueueComponenetBase.RootInstance.ReceiveCall(call));
            return setup;
        }
    }
}