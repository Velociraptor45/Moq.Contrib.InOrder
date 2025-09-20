using Moq.Language;
using Moq.Language.Flow;
using System;

namespace Moq.Contrib.InOrder.Setups;

public interface IOrderedSetup<TMock, TResult> : IThrows, IReturns<TMock, TResult>, ICallback<TMock, TResult> where TMock : class
{
}

public interface IOrderedSetup<TMock> : ICallBase, IThrows, IRaise<TMock>, ICallback where TMock : class
{
}

public interface IOrderedSetupGetter<TMock, TProperty> : IThrows, IReturnsGetter<TMock, TProperty>, ICallbackGetter<TMock, TProperty> where TMock : class
{
}

public interface IOrderedSetupSetter<TMock, TProperty> : ICallBase, IThrows, IRaise<TMock>, ICallbackSetter<TProperty> where TMock : class
{
}

internal class OrderedSetupSetter<TMock, TProperty> : IOrderedSetupSetter<TMock, TProperty> where TMock : class
{
    private readonly ISetupSetter<TMock, TProperty> _setup;
    private readonly Action _registerCallAction;

    public OrderedSetupSetter(ISetupSetter<TMock, TProperty> setup, Action registerCallAction)
    {
        _setup = setup;
        _registerCallAction = registerCallAction;
    }

    public ICallBaseResult CallBase()
    {
        return _setup.CallBase();
    }

    public IThrowsResult Throws(Exception exception)
    {
        return _setup.Throws(exception);
    }

    public IThrowsResult Throws<TException>() where TException : Exception, new()
    {
        return _setup.Throws<TException>();
    }

    public IThrowsResult Throws(Delegate exceptionFunction)
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<TException>(Func<TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T, TException>(Func<T, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, TException>(Func<T1, T2, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, TException>(Func<T1, T2, T3, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, TException>(Func<T1, T2, T3, T4, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, TException>(Func<T1, T2, T3, T4, T5, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, TException>(Func<T1, T2, T3, T4, T5, T6, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, TException>(Func<T1, T2, T3, T4, T5, T6, T7, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IVerifies Raises(Action<TMock> eventExpression, EventArgs args)
    {
        return _setup.Raises(eventExpression, args);
    }

    public IVerifies Raises(Action<TMock> eventExpression, Func<EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises(Action<TMock> eventExpression, params object[] args)
    {
        return _setup.Raises(eventExpression, args);
    }

    public IVerifies Raises<T1>(Action<TMock> eventExpression, Func<T1, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2>(Action<TMock> eventExpression, Func<T1, T2, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3>(Action<TMock> eventExpression, Func<T1, T2, T3, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<TMock> eventExpression,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public ICallbackResult Callback(Action<TProperty> action)
    {
        return _setup.Callback(p =>
        {
            _registerCallAction();
            action(p);
        });
    }
}

internal class OrderedSetupGetter<TMock, TProperty> : IOrderedSetupGetter<TMock, TProperty> where TMock : class
{
    private readonly ISetupGetter<TMock, TProperty> _setup;
    private readonly Action _registerCallAction;

    public OrderedSetupGetter(ISetupGetter<TMock, TProperty> setup, Action registerCallAction)
    {
        _setup = setup;
        _registerCallAction = registerCallAction;
    }

    public IThrowsResult Throws(Exception exception)
    {
        return _setup.Throws(exception);
    }

    public IThrowsResult Throws<TException>() where TException : Exception, new()
    {
        return _setup.Throws<TException>();
    }

    public IThrowsResult Throws(Delegate exceptionFunction)
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<TException>(Func<TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T, TException>(Func<T, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, TException>(Func<T1, T2, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, TException>(Func<T1, T2, T3, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, TException>(Func<T1, T2, T3, T4, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, TException>(Func<T1, T2, T3, T4, T5, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, TException>(Func<T1, T2, T3, T4, T5, T6, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, TException>(Func<T1, T2, T3, T4, T5, T6, T7, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IReturnsResult<TMock> Returns(TProperty value)
    {
        return _setup.Returns(value);
    }

    public IReturnsResult<TMock> Returns(Func<TProperty> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> CallBase()
    {
        return _setup.CallBase();
    }

    public IReturnsThrowsGetter<TMock, TProperty> Callback(Action action)
    {
        return _setup.Callback(() =>
        {
            _registerCallAction();
            action();
        });
    }
}

internal class OrderedSetup<TMock, TResult> : IOrderedSetup<TMock, TResult> where TMock : class
{
    private readonly ISetup<TMock, TResult> _setup;
    private readonly Action _registerCallAction;

    public OrderedSetup(ISetup<TMock, TResult> setup, Action registerCallAction)
    {
        _setup = setup;
        _registerCallAction = registerCallAction;
    }

    public IThrowsResult Throws(Exception exception)
    {
        return _setup.Throws(exception);
    }

    public IThrowsResult Throws<TException>() where TException : Exception, new()
    {
        return _setup.Throws<TException>();
    }

    public IThrowsResult Throws(Delegate exceptionFunction)
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<TException>(Func<TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T, TException>(Func<T, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, TException>(Func<T1, T2, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, TException>(Func<T1, T2, T3, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, TException>(Func<T1, T2, T3, T4, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, TException>(Func<T1, T2, T3, T4, T5, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, TException>(Func<T1, T2, T3, T4, T5, T6, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, TException>(Func<T1, T2, T3, T4, T5, T6, T7, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IReturnsResult<TMock> Returns(TResult value)
    {
        return _setup.Returns(value);
    }

    public IReturnsResult<TMock> Returns(InvocationFunc valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns(Delegate valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns(Func<TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T>(Func<T, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> CallBase()
    {
        return _setup.CallBase();
    }

    public IReturnsResult<TMock> Returns<T1, T2>(Func<T1, T2, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3>(Func<T1, T2, T3, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> valueFunction)
    {
        return _setup.Returns(valueFunction);
    }

    public IReturnsThrows<TMock, TResult> Callback(InvocationAction action)
    {
        throw new NotSupportedException(
            "Callback with InvocationAction is not supported in ordered setups. Use Action or Func overloads instead.");
    }

    public IReturnsThrows<TMock, TResult> Callback(Delegate callback)
    {
        Delegate chained = Delegate.CreateDelegate(
            callback.GetType(),
            (Action<object[]>)((args) =>
            {
                _registerCallAction();
                callback.DynamicInvoke(args);
            }),
            "Invoke"
        );

        return _setup.Callback(chained);
    }

    public IReturnsThrows<TMock, TResult> Callback(Action action)
    {
        return _setup.Callback(() =>
        {
            _registerCallAction();
            action();
        });
    }

    public IReturnsThrows<TMock, TResult> Callback<T>(Action<T> action)
    {
        return _setup.Callback<T>(x =>
        {
            _registerCallAction();
            action(x);
        });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2>(Action<T1, T2> action)
    {
        return _setup.Callback<T1, T2>((x1, x2) =>
        {
            _registerCallAction();
            action(x1, x2);
        });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3>(Action<T1, T2, T3> action)
    {
        return _setup.Callback<T1, T2, T3>(
            (x1, x2, x3) =>
            {
                _registerCallAction();
                action(x1, x2, x3);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
    {
        return _setup.Callback<T1, T2, T3, T4>(
            (x1, x2, x3, x4) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5>(
            (x1, x2, x3, x4, x5) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6>(
            (x1, x2, x3, x4, x5, x6) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7>(
            (x1, x2, x3, x4, x5, x6, x7) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8>(
            (x1, x2, x3, x4, x5, x6, x7, x8) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15);
            });
    }

    public IReturnsThrows<TMock, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16);
            });
    }
}

internal class OrderedSetup<TMock> : IOrderedSetup<TMock> where TMock : class
{
    private readonly ISetup<TMock> _setup;
    private readonly Action _registerCallAction;

    public OrderedSetup(ISetup<TMock> setup, Action registerCallAction)
    {
        _setup = setup;
        _registerCallAction = registerCallAction;
    }

    public ICallBaseResult CallBase()
    {
        return _setup.CallBase();
    }

    public IThrowsResult Throws(Exception exception)
    {
        return _setup.Throws(exception);
    }

    public IThrowsResult Throws<TException>() where TException : Exception, new()
    {
        return _setup.Throws<TException>();
    }

    public IThrowsResult Throws(Delegate exceptionFunction)
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<TException>(Func<TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T, TException>(Func<T, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, TException>(Func<T1, T2, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, TException>(Func<T1, T2, T3, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, TException>(Func<T1, T2, T3, T4, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, TException>(Func<T1, T2, T3, T4, T5, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, TException>(Func<T1, T2, T3, T4, T5, T6, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, TException>(Func<T1, T2, T3, T4, T5, T6, T7, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException>(
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException> exceptionFunction) where TException : Exception
    {
        return _setup.Throws(exceptionFunction);
    }

    public IVerifies Raises(Action<TMock> eventExpression, EventArgs args)
    {
        return _setup.Raises(eventExpression, args);
    }

    public IVerifies Raises(Action<TMock> eventExpression, Func<EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises(Action<TMock> eventExpression, params object[] args)
    {
        return _setup.Raises(eventExpression, args);
    }

    public IVerifies Raises<T1>(Action<TMock> eventExpression, Func<T1, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2>(Action<TMock> eventExpression, Func<T1, T2, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3>(Action<TMock> eventExpression, Func<T1, T2, T3, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<TMock> eventExpression,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, EventArgs> func)
    {
        return _setup.Raises(eventExpression, func);
    }

    public ICallbackResult Callback(InvocationAction action)
    {
        throw new NotSupportedException(
            "Callback with InvocationAction is not supported in ordered setups. Use Action or Func overloads instead.");
    }

    public ICallbackResult Callback(Delegate callback)
    {
        Delegate chained = Delegate.CreateDelegate(
            callback.GetType(),
            (Action<object[]>)((args) =>
            {
                _registerCallAction();
                callback.DynamicInvoke(args);
            }),
            "Invoke"
        );

        return _setup.Callback(chained);
    }

    public ICallbackResult Callback(Action action)
    {
        return _setup.Callback(() =>
        {
            _registerCallAction();
            action();
        });
    }

    public ICallbackResult Callback<T>(Action<T> action)
    {
        return _setup.Callback<T>(x =>
        {
            _registerCallAction();
            action(x);
        });
    }

    public ICallbackResult Callback<T1, T2>(Action<T1, T2> action)
    {
        return _setup.Callback<T1, T2>((x1, x2) =>
        {
            _registerCallAction();
            action(x1, x2);
        });
    }

    public ICallbackResult Callback<T1, T2, T3>(Action<T1, T2, T3> action)
    {
        return _setup.Callback<T1, T2, T3>(
            (x1, x2, x3) =>
            {
                _registerCallAction();
                action(x1, x2, x3);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
    {
        return _setup.Callback<T1, T2, T3, T4>(
            (x1, x2, x3, x4) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5>(
            (x1, x2, x3, x4, x5) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6>(
            (x1, x2, x3, x4, x5, x6) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7>(
            (x1, x2, x3, x4, x5, x6, x7) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8>(
            (x1, x2, x3, x4, x5, x6, x7, x8) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15);
            });
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
    {
        return _setup.Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16) =>
            {
                _registerCallAction();
                action(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16);
            });
    }
}