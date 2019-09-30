using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CreateCustomerShouldSucceed()
        {
            // Arrange
            var name = "Customer";

            // Act
            var result = new Customer(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateCustomerShouldFailWhenNameIsStringEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new Customer(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateCustomerShouldFailWhenNameIsNull()
        {
            // Arrange
            string name = null;

            // Act
            new Customer(name);
        }

        [TestMethod]
        public void TwoCustomersWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var name = "Customer";

            var first = new Customer(name);
            var second = new Customer(name);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoCustomersWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var name = "Customer";

            var first = new Customer(name);
            var second = new Customer(name);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoCustomersWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var firstName = "LB1227";
            var secondName = "LUITHAGEN";
            var first = new Customer(firstName);
            var second = new Customer(secondName);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoCustomersWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var firstName = "Customer 1";
            var secondName = "Customer 2";
            var first = new Customer(firstName);
            var second = new Customer(secondName);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

