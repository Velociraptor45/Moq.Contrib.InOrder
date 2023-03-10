# Moq.Contrib.InOrder

[![Nuget](https://img.shields.io/nuget/dt/Moq.Contrib.InOrder?color=blue&label=NuGet)](https://www.nuget.org/packages/Moq.Contrib.InOrder)

Moq.Contrib.InOrder is a library for ensuring that mock methods are called in correct order while strictly separating the acting & verification phase.

Register all mock setups that should be taken into account when verifying the call order in a `var queue = CallQueue.Create(x => { /* here */});`. To mark a mock method as relevant for order verification, use `.SetupInOrder(...)` instead of the normal `.Setup(...)` provided by moq (for every of moq's `Setup` methods, there is an `InOrder` version, e.g. `SetupGetInOrder`, `SetupAddInOrder`, etc).
Verify the call order after executing your test method by `queue.VerifyOrder()`.

# Examples

``` c#
// Arrange
var mock = new Mock<IDummy>();
var sut = new TestedDummyClass(mock);

var queue = CallQueue.Create(q =>
{
    mock.SetupInOrder(x => x.ExecuteAction("a"));
    mock.SetupInOrder(x => x.ExecuteAction("b"));
});

// Act
sut.TestedDummyMethod();

// Assert
queue.VerifyOrder();

/*
    The only call order that will satisfy queue.VerifyOrder():
    - ExecuteAction("a")
    - ExecuteAction("b")
*/
```

To define a loop for multiple setups, use `.RegisterLoop(...)`. To define a loop for a single setup, use the `times` property of ever `.SetupInOrder(...)` method.

``` c#
// Arrange
var mock = new Mock<IDummy>();
var sut = new TestedDummyClass(mock);

var queue = CallQueue.Create(q =>
{
    mock.SetupInOrder(x => x.ExecuteAction("a"));
    mock.SetupInOrder(x => x.ExecuteAction("b"), Times.Exactly(3));

    q.RegisterLoop(_ =>
    {
        // these two methods should be called in a loop which is expected to execute twice
        mock.SetupInOrder(x => x.ExecuteAction("c"));
        mock.SetupInOrder(x => x.ExecuteAction("d"));
    }, Times.Exactly(2));
});

// Act
sut.TestedDummyMethod();

// Assert
queue.VerifyOrder();

/*
    The only call order that will satisfy queue.VerifyOrder():
    - ExecuteAction("a")
    - ExecuteAction("b")
    - ExecuteAction("b")
    - ExecuteAction("b")
    - ExecuteAction("c")
    - ExecuteAction("d")
    - ExecuteAction("c")
    - ExecuteAction("d")
*/
```

You can also stack loop inside of loops.

```c#
var queue = CallQueue.Create(q =>
{
    q.RegisterLoop(loop =>
    {
        mock.SetupInOrder(x => x.ExecuteAction("c"));
        mock.SetupInOrder(x => x.ExecuteAction("d"));

        loop.RegisterLoop(_ =>
        {
            mock.SetupInOrder(x => x.ExecuteAction("e"));
            mock.SetupInOrder(x => x.ExecuteAction("f"));
        }, Times.AtLeast(2));
    }, Times.AtMost(2));
});
```

## Logging
Optionally, you can pass an `ILogger<CallQueue>` into `CallQueue.Create` that will log the expected and received calls.

```c#
var mock = new Mock<IDummy>();
var queue = CallQueue.Create(x0 =>
    {
        mock.SetupInOrder(x => x.ExecuteAction("a"));
        mock.SetupInOrder(x => x.ExecuteAction("b"), Times.AtLeast(2));

        x0.RegisterLoop(_ =>
        {
            mock.SetupInOrder(x => x.ExecuteAction("c"));
            mock.SetupInOrder(x => x.ExecuteAction("d"));
        }, Times.Exactly(2));
    },
    _logger);

mock.Object.ExecuteAction("a");
mock.Object.ExecuteAction("b");
mock.Object.ExecuteAction("b");

mock.Object.ExecuteAction("c");
mock.Object.ExecuteAction("d");

mock.Object.ExecuteAction("c");
mock.Object.ExecuteAction("d");
```
Producing the following output:
```
Information [0]: Expected Calls:
Information [0]: 	x => x.ExecuteAction("a")
Information [0]: 	(>2 times) x => x.ExecuteAction("b")
Information [0]: 	Loop: (2 times) 
Information [0]: 		x => x.ExecuteAction("c")
Information [0]: 		x => x.ExecuteAction("d")
Information [0]: ----------------------------
Information [0]: Received Calls:
Information [0]: 	x => x.ExecuteAction("a")
Information [0]: 	x => x.ExecuteAction("b")
Information [0]: 	x => x.ExecuteAction("b")
Information [0]: 	x => x.ExecuteAction("c")
Information [0]: 	x => x.ExecuteAction("d")
Information [0]: 	x => x.ExecuteAction("c")
Information [0]: 	x => x.ExecuteAction("d")
```
