using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class TransportTypeTests
    {
        [TestMethod]
        public void CreateTransportTypeShouldSucceed()
        {
            // Arrange
            var name = "Transport Type";

            // Act
            var result = new TransportType(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTransportTypeShouldFailWhenNameIsStringEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new TransportType(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTransportTypeShouldFailWhenNameIsNull()
        {
            // Arrange
            string name = null;

            // Act
            new TransportType(name);
        }

        [TestMethod]
        public void TwoTransportTypesWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var name = "Transport Type";

            var first = new TransportType(name);
            var second = new TransportType(name);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoTransportTypesWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var name = "Transport Type";

            var first = new TransportType(name);
            var second = new TransportType(name);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoTransportTypesWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var firstName = "Transport Type 1";
            var secondName = "Transport Type 2";
            var first = new TransportType(firstName);
            var second = new TransportType(secondName);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoTransportTypesWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var firstName = "Transport Type 1";
            var secondName = "Transport Type 2";
            var first = new ProductionSite(firstName);
            var second = new ProductionSite(secondName);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

