using FluentAssertions;
using Moq.Contrib.InOrder.Extensions;

namespace Moq.Contrib.InOrder.Tests.Extensions;

public class MoqExtensionsTests
{
    [Fact]
    public void SetupInOrder_Action_OutsideOfCallQueue_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        // Act
        Action act = () => mock.SetupInOrder(x => x.ExecuteAction());

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void SetupInOrder_Func_OutsideOfCallQueue_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        // Act
        Action act = () => mock.SetupInOrder(x => x.ExecuteFunc());

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void SetupGetInOrder_OutsideOfCallQueue_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        // Act
        Action act = () => mock.SetupGetInOrder(x => x.MyProperty);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void SetupSetInOrder_OutsideOfCallQueue_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        // Act
        Action act = () => mock.SetupSetInOrder(x => x.MyProperty = 4);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void SetupSetInOrder_Casted_OutsideOfCallQueue_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        // Act
        Action act = () => mock.SetupSetInOrder<IDummy, int>(x => x.MyProperty = 4);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void SetupAddInOrder_OutsideOfCallQueue_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        // Act
        Action act = () => mock.SetupAddInOrder(x => x.EventHandler += DummyClass.DummyMethod);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void SetupRemoveInOrder_OutsideOfCallQueue_ShouldNotThrow()
    {
        // Arrange
        var mock = new Mock<IDummy>();

        // Act
        Action act = () => mock.SetupRemoveInOrder(x => x.EventHandler -= DummyClass.DummyMethod);

        // Assert
        act.Should().NotThrow();
    }

    public interface IDummy
    {
        public int MyProperty { get; set; }

        public event EventHandler EventHandler;

        void ExecuteAction();

        int ExecuteFunc();
    }

    public class DummyClass
    {
        public static void DummyMethod(object? o, EventArgs args)
        {
            // dummy
        }
    }
}