using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class OperationTests
    {
        [TestMethod]
        public void CreateOperationShouldSucceed()
        {
            // Arrange
            var name = "Test";

            // Act
            var result = new Operation(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateOperationShouldFailWhenNameEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new Operation(name);
        }

        [TestMethod]
        public void TwoOperationsWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var name = "Test";

            var first = new Operation(name);
            var second = new Operation(name);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoOperationsWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var name = "Test";

            var first = new Operation(name);
            var second = new Operation(name);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoOperationsWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var nameOperation1 = "Test1";
            var nameOperation2 = "Test2";
            var first = new Operation(nameOperation1);
            var second = new Operation(nameOperation2);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoOperationsWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var nameOperation1 = "Test1";
            var nameOperation2 = "Test2";
            var first = new Operation(nameOperation1);
            var second = new Operation(nameOperation2);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

