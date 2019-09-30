using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Domain.Extensions
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void ToPrettyStringShouldReturnProperTypeAsString()
        {
            // Arrange
            var type = typeof(TeamId);

            // Act
            var expected = type.ToPrettyString();

            // Assert
            expected.Should().Be("TeamId");
        }

        [TestMethod]
        public void ToPrettyStringShouldReturnProperGenericTypeAsString()
        {
            // Arrange
            var type = typeof(Identity<>);

            // Act
            var expected = type.ToPrettyString();

            // Assert
            expected.Should().Be("Identity<>");
        }

        [TestMethod]
        public void ToPrettyStringShouldReturnProperGenericTypeTypeAsString()
        {
            // Arrange
            var type = typeof(Identity<TeamId>);

            // Act
            var expected = type.ToPrettyString();

            // Assert
            expected.Should().Be("Identity<TeamId>");
        }
    }
}
