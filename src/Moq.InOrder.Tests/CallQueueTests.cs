using FluentAssertions;
using Moq.InOrder.Exceptions;
using Moq.InOrder.Extensions;

namespace Moq.InOrder.Tests;

public class CallQueueTests
{
    [Fact]
    public void VerifyOrder_CallsAndMultiLoop_ValidCalls_ShouldNotThrow()
    {
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupInOrder(x => x.ExecuteAction("a"));
            mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));

            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
            }, Times.Exactly(2));
        });

        mock.Object.ExecuteAction("a");
        mock.Object.ExecuteAction(new DummyClass("x"));

        mock.Object.ExecuteAction(new DummyClass("y"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        mock.Object.ExecuteAction(new DummyClass("y"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_CallsAndMultiLoop_OneLoopMissing_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupInOrder(x => x.ExecuteAction("a"));
            mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));

            x0.RegisterLoop(_ =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
            }, Times.Exactly(2));
        });

        mock.Object.ExecuteAction("a");
        mock.Object.ExecuteAction(new DummyClass("x"));

        mock.Object.ExecuteAction(new DummyClass("y"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>().WithMessage(
            "Expected loop\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"y\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S))\r\nexactly 2 time(s) but received it 1 time(s)");
    }

    [Fact]
    public void VerifyOrder_ExceedingLoopTimes_Complete_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
            }, Times.Once());
        });

        mock.Object.ExecuteAction(new DummyClass("y"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        mock.Object.ExecuteAction(new DummyClass("y"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>().WithMessage(
            "Expected loop\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"y\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S))\r\nexactly 1 time(s) but received it 2 time(s)");
    }

    [Fact]
    public void VerifyOrder_ExceedingLoopTimes_Partial_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
            }, Times.Once());
        });

        mock.Object.ExecuteAction(new DummyClass("y"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        mock.Object.ExecuteAction(new DummyClass("y"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>().WithMessage(
            "Loop\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"y\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S))\r\nis missing x => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S)) after 1 complete run(s)");
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(3, 5)]
    public void VerifyOrder_LoopAtLeastCount_MeetExpectedCount_ShouldNotThrow(int atLeaseExpectedLoopCount,
        int actualLoopCount)
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
            }, Times.AtLeast(atLeaseExpectedLoopCount));
        });

        for (int i = 0; i < actualLoopCount; i++)
        {
            mock.Object.ExecuteAction(new DummyClass("a"));
            mock.Object.ExecuteAction(new DummyClass("b"));
        }

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_LoopAtLeastCount_NotReachExpectedCount_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
            }, Times.AtLeast(2));
        });

        mock.Object.ExecuteAction(new DummyClass("a"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>().WithMessage(
            "Expected loop\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"a\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S))\r\nbetween 2 and 2147483647 time(s) but received it 1 time(s)");
    }

    [Theory]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    public void VerifyOrder_LoopAtMostCount_MeetExpectedCount_ShouldNotThrow(int atMostExpectedLoopCount,
        int actualLoopCount)
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
            }, Times.AtMost(atMostExpectedLoopCount));
        });

        for (int i = 0; i < actualLoopCount; i++)
        {
            mock.Object.ExecuteAction(new DummyClass("a"));
            mock.Object.ExecuteAction(new DummyClass("b"));
        }

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_LoopAtMostCount_ExceedingExpectedCount_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
            }, Times.AtMost(1));
        });

        mock.Object.ExecuteAction(new DummyClass("a"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        mock.Object.ExecuteAction(new DummyClass("a"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>().WithMessage(
            "Expected loop\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"a\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S))\r\nbetween 0 and 1 time(s) but received it 2 time(s)");
    }

    [Fact]
    public void VerifyOrder_LoopInLoop_ValidCalls_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));

                x1.RegisterLoop(x1 =>
                {
                    mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));
                    mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
                });
            });
        });

        mock.Object.ExecuteAction(new DummyClass("x"));
        mock.Object.ExecuteAction(new DummyClass("y"));

        mock.Object.ExecuteAction(new DummyClass("a"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_LoopInLoop_OuterLoopCallMissing_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));

                x1.RegisterLoop(x1 =>
                {
                    mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));
                    mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
                });
            });
        });

        mock.Object.ExecuteAction(new DummyClass("y"));

        mock.Object.ExecuteAction(new DummyClass("a"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>().WithMessage(
            "Expected loop\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"x\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"y\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"a\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S))\r\nexactly 1 time(s) but received it 0 time(s)");
    }

    [Fact]
    public void VerifyOrder_LoopInLoop_InnerLoopRegisteredOnQueue_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(q =>
        {
            q.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));

                q.RegisterLoop(x1 =>
                {
                    mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));
                    mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
                }, Times.Exactly(2));
            }, Times.Exactly(2));
        });

        mock.Object.ExecuteAction(new DummyClass("x"));
        mock.Object.ExecuteAction(new DummyClass("y"));
        mock.Object.ExecuteAction(new DummyClass("x"));
        mock.Object.ExecuteAction(new DummyClass("y"));

        mock.Object.ExecuteAction(new DummyClass("a"));
        mock.Object.ExecuteAction(new DummyClass("b"));
        mock.Object.ExecuteAction(new DummyClass("a"));
        mock.Object.ExecuteAction(new DummyClass("b"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_LoopInLoop_InnerLoopCallMissing_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));

                x1.RegisterLoop(x1 =>
                {
                    mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));
                    mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("b").S)));
                });
            });
        });

        mock.Object.ExecuteAction(new DummyClass("x"));
        mock.Object.ExecuteAction(new DummyClass("y"));

        mock.Object.ExecuteAction(new DummyClass("a"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>().WithMessage(
            "Loop\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"a\").S)),\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S))\r\nis missing x => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"b\").S)) after 0 complete run(s)");
    }

    [Fact]
    public void VerifyOrder_Loop_OnlyOneCall_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));
            });
        });

        mock.Object.ExecuteAction(new DummyClass("x"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(
            "Loops with only one call are currently not supported. Please use the 'times' argument on '.SetupInOrder()' instead");
    }

    [Fact]
    public void VerifyOrder_TooManyCalls_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));
            mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));
        });

        mock.Object.ExecuteAction(new DummyClass("x"));
        mock.Object.ExecuteAction(new DummyClass("y"));
        mock.Object.ExecuteAction(new DummyClass("x"));

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>().WithMessage(
            "All setups satisfied but the following calls are remaining and missing a corresponding setup:\r\nx => x.ExecuteAction(It.Is<CallQueueTests.DummyClass>(c => c.S == new CallQueueTests.DummyClass(\"x\").S))");
    }

    [Fact]
    public void Create_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        // Act
        Action act = () => CallQueue.Create(x0 =>
        {
            mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("a").S)));

            x0.RegisterLoop(x1 =>
            {
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("x").S)));
                mock.SetupInOrder(x => x.ExecuteAction(It.Is<DummyClass>(c => c.S == new DummyClass("y").S)));
            });
        });

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_GetProperty_OneCall_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupGetInOrder(x => x.MyProperty).Returns(5);
        });

        _ = mock.Object.MyProperty;

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_GetProperty_TwoCalls_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupGetInOrder(x => x.MyProperty, Times.Exactly(2)).Returns(5);
        });

        _ = mock.Object.MyProperty;
        _ = mock.Object.MyProperty;

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_GetProperty_OneCallTooMany_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupGetInOrder(x => x.MyProperty).Returns(5);
        });

        _ = mock.Object.MyProperty;
        _ = mock.Object.MyProperty;

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>()
            .WithMessage("Expected x => x.MyProperty exactly 1 time(s) but received it 2 time(s)");
    }

    [Fact]
    public void VerifyOrder_GetProperty_OnlySetCalled_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupGetInOrder(x => x.MyProperty).Returns(5);
        });

        mock.Object.MyProperty = 5;

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>()
            .WithMessage("Expected x => x.MyProperty exactly 1 time(s) but received it 0 time(s)");
    }

    [Fact]
    public void VerifyOrder_SetProperty_OneCall_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupSetInOrder(x => x.MyProperty = 5);
        });

        mock.Object.MyProperty = 5;

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_SetProperty_TwoCalls_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupSetInOrder(x => x.MyProperty = 5, Times.Exactly(2));
        });

        mock.Object.MyProperty = 5;
        mock.Object.MyProperty = 5;

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void VerifyOrder_SetProperty_OneCallTooMany_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupSetInOrder(x => x.MyProperty = 5);
        });

        mock.Object.MyProperty = 5;
        mock.Object.MyProperty = 5;

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>()
            .WithMessage("Expected x => x.MyProperty = 5 exactly 1 time(s) but received it 2 time(s)");
    }

    [Fact]
    public void VerifyOrder_SetProperty_OnlyGetCalled_ShouldThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        var queue = CallQueue.Create(x0 =>
        {
            mock.SetupSetInOrder(x => x.MyProperty = 5);
        });

        _ = mock.Object.MyProperty;

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>()
            .WithMessage("Expected x => x.MyProperty = 5 exactly 1 time(s) but received it 0 time(s)");
    }

    public interface IDummy
    {
        public int MyProperty { get; set; }

        void ExecuteAction(string s);

        void ExecuteAction(DummyClass c);
    }

    public class DummyClass
    {
        public DummyClass(string s)
        {
            S = s;
        }

        public string S { get; }
    }
}