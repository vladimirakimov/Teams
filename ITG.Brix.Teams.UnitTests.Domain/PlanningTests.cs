using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class PlanningTests
    {
        [TestMethod]
        public void CreatePlanningShouldSucceed()
        {
            // Arrange
            var name = "Planning";

            // Act
            var result = new TypePlanning(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePlanningShouldFailWhenNameEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new TypePlanning(name);
        }

        [TestMethod]
        public void TwoPlanningsWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var name = "Planning";

            var first = new TypePlanning(name);
            var second = new TypePlanning(name);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoPlanningsWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var name = "Planning";

            var first = new TypePlanning(name);
            var second = new TypePlanning(name);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoPlanningsWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var namePlanning1 = "Planning1";
            var namePlanning2 = "Planning2";
            var first = new TypePlanning(namePlanning1);
            var second = new TypePlanning(namePlanning2);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoPlanningsWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var namePlanning1 = "Planning1";
            var namePlanning2 = "Planning2";
            var first = new TypePlanning(namePlanning1);
            var second = new TypePlanning(namePlanning2);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

