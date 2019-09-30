using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class OperationalDepartmentTests
    {
        [TestMethod]
        public void CreateOperationalDepartmentShouldSucceed()
        {
            // Arrange
            var name = "Test";

            // Act
            var result = new OperationalDepartment(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateOperationalDepartmentShouldFailWhenNameEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new OperationalDepartment(name);
        }

        [TestMethod]
        public void TwoOperationalDepartmentsWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var name = "Test";

            var first = new OperationalDepartment(name);
            var second = new OperationalDepartment(name);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoOperationalDepartmentsWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var name = "Test";

            var first = new OperationalDepartment(name);
            var second = new OperationalDepartment(name);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoOperationalDepartmentsWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var name1 = "Test1";
            var name2 = "Test2";
            var first = new OperationalDepartment(name1);
            var second = new OperationalDepartment(name2);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoOperationalDepartmentsWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var name1 = "Test1";
            var name2 = "Test2";
            var first = new OperationalDepartment(name1);
            var second = new OperationalDepartment(name2);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

