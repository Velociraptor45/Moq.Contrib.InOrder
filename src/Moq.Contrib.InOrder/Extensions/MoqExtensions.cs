using Moq.Contrib.InOrder.Setups;
using System;
using System.Linq.Expressions;

namespace Moq.Contrib.InOrder.Extensions
{
    public static class MoqExtensions
    {
        extension<T>(Mock<T> mock) where T : class
        {
            public IOrderedSetup<T> SetupInOrder(Expression<Action<T>> expression,
                IQueueComponent component)
            {
                return mock.SetupInOrder(expression, Times.Once(), component);
            }

            public IOrderedSetup<T> SetupInOrder(Expression<Action<T>> expression,
                Times times, IQueueComponent component)
            {
                var setup = mock.Setup(expression);
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);

                var action = () => component.GetRoot().ReceiveCall(call);
                setup.Callback(action);
                return new OrderedSetup<T>(setup, action);
            }

            public IOrderedSetup<T, TResult> SetupInOrder<TResult>(Expression<Func<T, TResult>> expression, IQueueComponent component)
            {
                return mock.SetupInOrder(expression, Times.Once(), component);
            }

            public IOrderedSetup<T, TResult> SetupInOrder<TResult>(Expression<Func<T, TResult>> expression, Times times, IQueueComponent component)
            {
                var setup = mock.Setup(expression);
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
                var action = () => component.GetRoot().ReceiveCall(call);
                setup.Callback(action);
                return new OrderedSetup<T, TResult>(setup, action);
            }

            public IOrderedSetupGetter<T, TProperty> SetupGetInOrder<TProperty>(Expression<Func<T, TProperty>> expression, IQueueComponent component)
            {
                return mock.SetupGetInOrder(expression, Times.Once(), component);
            }

            public IOrderedSetupGetter<T, TProperty> SetupGetInOrder<TProperty>(Expression<Func<T, TProperty>> expression, Times times, IQueueComponent component)
            {
                var setup = mock.SetupGet(expression);
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
                var action = () => component.GetRoot().ReceiveCall(call);
                setup.Callback(action);
                return new OrderedSetupGetter<T, TProperty>(setup, action);
            }

            public IOrderedSetupSetter<T, TProperty> SetupSetInOrder<TProperty>(Action<T> expression, IQueueComponent component)
            {
                return mock.SetupSetInOrder<T, TProperty>(expression, Times.Once(), component);
            }

            public IOrderedSetupSetter<T, TProperty> SetupSetInOrder<TProperty>(Action<T> expression, Times times, IQueueComponent component)
            {
                var setup = mock.SetupSet<TProperty>(expression);
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
                setup.Callback(_ => component.GetRoot().ReceiveCall(call));
                return new OrderedSetupSetter<T, TProperty>(setup, () => component.GetRoot().ReceiveCall(call));
            }

            public IOrderedSetup<T> SetupSetInOrder(Action<T> expression, IQueueComponent component)
            {
                return mock.SetupSetInOrder(expression, Times.Once(), component);
            }

            public IOrderedSetup<T> SetupSetInOrder(Action<T> expression, Times times, IQueueComponent component)
            {
                var setup = mock.SetupSet(expression);
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
                var action = () => component.GetRoot().ReceiveCall(call);
                setup.Callback(action);
                return new OrderedSetup<T>(setup, action);
            }

            public IOrderedSetup<T> SetupAddInOrder(Action<T> expression, IQueueComponent component)
            {
                return mock.SetupAddInOrder(expression, Times.Once(), component);
            }

            public IOrderedSetup<T> SetupAddInOrder(Action<T> expression, Times times, IQueueComponent component)
            {
                var setup = mock.SetupAdd(expression);
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
                var action = () => component.GetRoot().ReceiveCall(call);
                setup.Callback(action);
                return new OrderedSetup<T>(setup, action);
            }

            public IOrderedSetup<T> SetupRemoveInOrder(Action<T> expression, IQueueComponent component)
            {
                return mock.SetupRemoveInOrder(expression, Times.Once(), component);
            }

            public IOrderedSetup<T> SetupRemoveInOrder(Action<T> expression, Times times, IQueueComponent component)
            {
                var setup = mock.SetupRemove(expression);
                var call = component.RegisterCall(mock.Name, setup.ToString(), times);
            
                var action = () => component.GetRoot().ReceiveCall(call);
                setup.Callback(action);
                return new OrderedSetup<T>(setup, action);
            }
        }
    }
}