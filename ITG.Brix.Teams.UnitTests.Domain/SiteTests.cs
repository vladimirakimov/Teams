using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class SiteTests
    {
        [TestMethod]
        public void CreateSiteShouldSucceed()
        {
            // Arrange
            var name = "LB1227";

            // Act
            var result = new Site(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateSiteShouldFailWhenNameIsStringEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new Site(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateSiteShouldFailWhenNameIsNull()
        {
            // Arrange
            string name = null;

            // Act
            new Site(name);
        }

        [TestMethod]
        public void TwoSitesWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var name = "Unload packed";

            var first = new Site(name);
            var second = new Site(name);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoSitesWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var name = "Unload packed";

            var first = new Site(name);
            var second = new Site(name);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoSitesWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var firstName = "LB1227";
            var secondName = "LUITHAGEN";
            var first = new Site(firstName);
            var second = new Site(secondName);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoSitesWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var firstName = "LB1227";
            var secondName = "LUITHAGEN";
            var first = new Site(firstName);
            var second = new Site(secondName);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

