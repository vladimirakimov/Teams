using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class ProductionSiteTests
    {
        [TestMethod]
        public void CreateProductionSiteShouldSucceed()
        {
            // Arrange
            var name = "ProductionSite";

            // Act
            var result = new ProductionSite(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateProductionSiteShouldFailWhenNameIsStringEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new ProductionSite(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateProductionSiteShouldFailWhenNameIsNull()
        {
            // Arrange
            string name = null;

            // Act
            new ProductionSite(name);
        }

        [TestMethod]
        public void TwoProductionSitesWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var name = "Production Site";

            var first = new ProductionSite(name);
            var second = new ProductionSite(name);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoProductionSitesWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var name = "Production Site";

            var first = new ProductionSite(name);
            var second = new ProductionSite(name);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoProductionSitesWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var firstName = "Production Site 1";
            var secondName = "Production Site 2";
            var first = new ProductionSite(firstName);
            var second = new ProductionSite(secondName);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoProductionSitesWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var firstName = "Production Site 1";
            var secondName = "Production Site 2";
            var first = new ProductionSite(firstName);
            var second = new ProductionSite(secondName);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

