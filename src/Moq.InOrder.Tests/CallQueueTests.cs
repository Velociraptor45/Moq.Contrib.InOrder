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

        // Act
        Action act = () => queue.VerifyOrder();

        // Assert
        act.Should().Throw<MoqOrderViolatedException>(); // todo message
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
        act.Should().Throw<MoqOrderViolatedException>(); // todo message
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
        act.Should().Throw<MoqOrderViolatedException>(); // todo message
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
        act.Should().Throw<MoqOrderViolatedException>(); // todo message
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
        act.Should().Throw<MoqOrderViolatedException>(); // todo message
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

    public interface IDummy
    {
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