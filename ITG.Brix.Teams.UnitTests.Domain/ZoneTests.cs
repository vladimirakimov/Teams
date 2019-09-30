using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class ZoneTests
    {
        [TestMethod]
        public void CreateZoneShouldSucceed()
        {
            // Arrange
            var name = "Zone";

            // Act
            var result = new Zone(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateZoneShouldFailWhenNameEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new Zone(name);
        }

        [TestMethod]
        public void TwoZonesWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var name = "Zone";

            var first = new Zone(name);
            var second = new Zone(name);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoZonesWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var name = "Zone";

            var first = new Zone(name);
            var second = new Zone(name);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoZonesWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var nameZone1 = "Zone1";
            var nameZone2 = "Zone2";
            var first = new Zone(nameZone1);
            var second = new Zone(nameZone2);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoZonesWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var nameZone1 = "Zone1";
            var nameZone2 = "Zone2";
            var first = new Zone(nameZone1);
            var second = new Zone(nameZone2);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

