using System;
using System.Linq.Expressions;

namespace Moq.Contrib.InOrder.Extensions
{
    public static class MoqExtensions
    {
        public static Language.Flow.ISetup<T> SetupInOrder<T>(this Mock<T> mock, Expression<Action<T>> expression,
            IQueueComponent? component)
            where T : class
        {
            return mock.SetupInOrder(expression, Times.Once(), component);
        }

        public static Language.Flow.ISetup<T> SetupInOrder<T>(this Mock<T> mock, Expression<Action<T>> expression,
            Times times, IQueueComponent? component) where T : class
        {
            var setup = mock.Setup(expression);
            if (component is not null)
            {
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
                setup.Callback(() => component.GetRoot().ReceiveCall(call));
            }
            return setup;
        }

        public static Language.Flow.ISetup<T, TResult> SetupInOrder<T, TResult>(this Mock<T> mock,
            Expression<Func<T, TResult>> expression, IQueueComponent? component) where T : class
        {
            return mock.SetupInOrder(expression, Times.Once(), component);
        }

        public static Language.Flow.ISetup<T, TResult> SetupInOrder<T, TResult>(this Mock<T> mock,
            Expression<Func<T, TResult>> expression, Times times, IQueueComponent? component) where T : class
        {
            var setup = mock.Setup(expression);
            if (component is not null)
            {
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
                setup.Callback(() => component.GetRoot().ReceiveCall(call));
            }
            return setup;
        }

        public static Language.Flow.ISetupGetter<T, TProperty> SetupGetInOrder<T, TProperty>(this Mock<T> mock,
            Expression<Func<T, TProperty>> expression, IQueueComponent? component) where T : class
        {
            return mock.SetupGetInOrder(expression, Times.Once(), component);
        }

        public static Language.Flow.ISetupGetter<T, TProperty> SetupGetInOrder<T, TProperty>(this Mock<T> mock,
            Expression<Func<T, TProperty>> expression, Times times, IQueueComponent? component) where T : class
        {
            var setup = mock.SetupGet(expression);
            if (component is not null)
            {
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
                setup.Callback(() => component.GetRoot().ReceiveCall(call));
            }
            return setup;
        }

        public static Language.Flow.ISetupSetter<T, TProperty> SetupSetInOrder<T, TProperty>(this Mock<T> mock,
            Action<T> expression, IQueueComponent? component) where T : class
        {
            return mock.SetupSetInOrder<T, TProperty>(expression, Times.Once(), component);
        }

        public static Language.Flow.ISetupSetter<T, TProperty> SetupSetInOrder<T, TProperty>(this Mock<T> mock,
            Action<T> expression, Times times, IQueueComponent? component) where T : class
        {
            var setup = mock.SetupSet<TProperty>(expression);
            if (component is not null)
            {
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
                setup.Callback(p => component.GetRoot().ReceiveCall(call));
            }
            return setup;
        }

        public static Language.Flow.ISetup<T> SetupSetInOrder<T>(this Mock<T> mock,
            Action<T> expression, IQueueComponent? component) where T : class
        {
            return mock.SetupSetInOrder(expression, Times.Once(), component);
        }

        public static Language.Flow.ISetup<T> SetupSetInOrder<T>(this Mock<T> mock,
            Action<T> expression, Times times, IQueueComponent? component) where T : class
        {
            var setup = mock.SetupSet(expression);
            if (component is not null)
            {
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
                setup.Callback(() => component.GetRoot().ReceiveCall(call));
            }
            return setup;
        }

        public static Language.Flow.ISetup<T> SetupAddInOrder<T>(this Mock<T> mock,
            Action<T> expression, IQueueComponent? component) where T : class
        {
            return mock.SetupAddInOrder(expression, Times.Once(), component);
        }

        public static Language.Flow.ISetup<T> SetupAddInOrder<T>(this Mock<T> mock,
            Action<T> expression, Times times, IQueueComponent? component) where T : class
        {
            var setup = mock.SetupAdd(expression);
            if (component is not null)
            {
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
                setup.Callback(() => component.GetRoot().ReceiveCall(call));
            }
            return setup;
        }

        public static Language.Flow.ISetup<T> SetupRemoveInOrder<T>(this Mock<T> mock,
            Action<T> expression, IQueueComponent? component) where T : class
        {
            return mock.SetupRemoveInOrder(expression, Times.Once(), component);
        }

        public static Language.Flow.ISetup<T> SetupRemoveInOrder<T>(this Mock<T> mock,
            Action<T> expression, Times times, IQueueComponent? component) where T : class
        {
            var setup = mock.SetupRemove(expression);
            if (component is not null)
            {
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
                setup.Callback(() => component.GetRoot().ReceiveCall(call));
            }
            return setup;
        }
    }
}