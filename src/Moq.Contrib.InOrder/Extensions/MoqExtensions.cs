using Moq.Contrib.InOrder.Setups;
using System;
using System.Linq.Expressions;

namespace Moq.Contrib.InOrder.Extensions
{
    public static class MoqExtensions
    {
        public static IOrderedSetup<T> SetupInOrder<T>(this Mock<T> mock, Expression<Action<T>> expression,
            IQueueComponent component)
            where T : class
        {
            return mock.SetupInOrder(expression, Times.Once(), component);
        }

        public static IOrderedSetup<T> SetupInOrder<T>(this Mock<T> mock, Expression<Action<T>> expression,
            Times times, IQueueComponent component) where T : class
        {
            var setup = mock.Setup(expression);
            var call = component.RegisterCall(mock.Name, setup.ToString(), times);

            var action = () => component.GetRoot().ReceiveCall(call);
            setup.Callback(action);
            return new OrderedSetup<T>(setup, action);
        }

        public static IOrderedSetup<T, TResult> SetupInOrder<T, TResult>(this Mock<T> mock,
            Expression<Func<T, TResult>> expression, IQueueComponent component) where T : class
        {
            return mock.SetupInOrder(expression, Times.Once(), component);
        }

        public static IOrderedSetup<T, TResult> SetupInOrder<T, TResult>(this Mock<T> mock,
            Expression<Func<T, TResult>> expression, Times times, IQueueComponent component) where T : class
        {
            var setup = mock.Setup(expression);
            var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
            var action = () => component.GetRoot().ReceiveCall(call);
            setup.Callback(action);
            return new OrderedSetup<T, TResult>(setup, action);
        }

        public static IOrderedSetupGetter<T, TProperty> SetupGetInOrder<T, TProperty>(this Mock<T> mock,
            Expression<Func<T, TProperty>> expression, IQueueComponent component) where T : class
        {
            return mock.SetupGetInOrder(expression, Times.Once(), component);
        }

        public static IOrderedSetupGetter<T, TProperty> SetupGetInOrder<T, TProperty>(this Mock<T> mock,
            Expression<Func<T, TProperty>> expression, Times times, IQueueComponent component) where T : class
        {
            var setup = mock.SetupGet(expression);
            var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
            var action = () => component.GetRoot().ReceiveCall(call);
            setup.Callback(action);
            return new OrderedSetupGetter<T, TProperty>(setup, action);
        }

        public static IOrderedSetupSetter<T, TProperty> SetupSetInOrder<T, TProperty>(this Mock<T> mock,
            Action<T> expression, IQueueComponent component) where T : class
        {
            return mock.SetupSetInOrder<T, TProperty>(expression, Times.Once(), component);
        }

        public static IOrderedSetupSetter<T, TProperty> SetupSetInOrder<T, TProperty>(this Mock<T> mock,
            Action<T> expression, Times times, IQueueComponent component) where T : class
        {
            var setup = mock.SetupSet<TProperty>(expression);
            var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
            setup.Callback(_ => component.GetRoot().ReceiveCall(call));
            return new OrderedSetupSetter<T, TProperty>(setup, () => component.GetRoot().ReceiveCall(call));
        }

        public static IOrderedSetup<T> SetupSetInOrder<T>(this Mock<T> mock,
            Action<T> expression, IQueueComponent component) where T : class
        {
            return mock.SetupSetInOrder(expression, Times.Once(), component);
        }

        public static IOrderedSetup<T> SetupSetInOrder<T>(this Mock<T> mock,
            Action<T> expression, Times times, IQueueComponent component) where T : class
        {
            var setup = mock.SetupSet(expression);
            var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
            var action = () => component.GetRoot().ReceiveCall(call);
            setup.Callback(action);
            return new OrderedSetup<T>(setup, action);
        }

        public static IOrderedSetup<T> SetupAddInOrder<T>(this Mock<T> mock,
            Action<T> expression, IQueueComponent component) where T : class
        {
            return mock.SetupAddInOrder(expression, Times.Once(), component);
        }

        public static IOrderedSetup<T> SetupAddInOrder<T>(this Mock<T> mock,
            Action<T> expression, Times times, IQueueComponent component) where T : class
        {
            var setup = mock.SetupAdd(expression);
            var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
            var action = () => component.GetRoot().ReceiveCall(call);
            setup.Callback(action);
            return new OrderedSetup<T>(setup, action);
        }

        public static IOrderedSetup<T> SetupRemoveInOrder<T>(this Mock<T> mock,
            Action<T> expression, IQueueComponent component) where T : class
        {
            return mock.SetupRemoveInOrder(expression, Times.Once(), component);
        }

        public static IOrderedSetup<T> SetupRemoveInOrder<T>(this Mock<T> mock,
            Action<T> expression, Times times, IQueueComponent component) where T : class
        {
            var setup = mock.SetupRemove(expression);
            var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
            var action = () => component.GetRoot().ReceiveCall(call);
            setup.Callback(action);
            return new OrderedSetup<T>(setup, action);
        }
    }
}