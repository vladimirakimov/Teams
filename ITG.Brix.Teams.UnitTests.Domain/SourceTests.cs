using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class SourceTests
    {
        [TestMethod]
        public void CreateSourceShouldSucceed()
        {
            // Arrange
            var sourceName = "Test";

            // Act
            var result = new Source(sourceName);

            // Assert
            result.Name.Should().Be(sourceName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSourceShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var sourceName = string.Empty;

            // Act
            new Source(sourceName);
        }

        [TestMethod]
        public void TwoSourcesWithSameValuesShouldBeEqualThroughMethod()
        {
            // Arrange
            var sourceName = "Test";

            var first = new Source(sourceName);
            var second = new Source(sourceName);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoSourcesWithSameValuesShouldBeEqualThroughOperator()
        {
            // Arrange
            var sourceName = "Test";

            var first = new Source(sourceName);
            var second = new Source(sourceName);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TwoSourcesWithDistinctValuesShouldBeNotEqualThroughMethod()
        {
            // Arrange
            var sourceNameFirst = "Test1";
            var sourceNameSecond = "Test2";
            var first = new Source(sourceNameFirst);
            var second = new Source(sourceNameSecond);

            // Act
            var result = first.Equals(second);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoSourcesWithDistinctValuesShouldBeNotEqualThroughOperator()
        {
            // Arrange
            var sourceNameFirst = "Test1";
            var sourceNameSecond = "Test2";
            var first = new Source(sourceNameFirst);
            var second = new Source(sourceNameSecond);

            // Act
            var result = first != second;

            // Assert
            result.Should().BeTrue();
        }
    }
}

