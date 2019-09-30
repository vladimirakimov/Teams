using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class TeamIdTests
    {
        [TestMethod]
        public void GuidToAndFromTeamIdShouldSucceed()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var teamId = TeamId.With(guid);
            var result = teamId.GetGuid();

            // Assert
            result.Should().Be(guid);
        }

        [TestMethod]
        public void TeamIdNewValueShouldContainLowerCasedValue()
        {
            // Act
            var teamId = TeamId.New;

            // Assert
            teamId.Value.Should().Be(teamId.Value.ToLowerInvariant());
        }

        [TestMethod]
        public void TeamIdNewValidShouldBeValid()
        {
            // Arrange
            var teamId = TeamId.New;

            // Act
            var result = TeamId.IsValid(teamId.Value);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void TeamIdNewValidShouldBeNotValid()
        {
            // Arrange
            var value = string.Empty;

            // Act
            var result = TeamId.IsValid(value);

            // Assert
            result.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow("someInvalidGuid", DisplayName = "Invalid Guid")]
        [DataRow(" eb644f96-e978-4d49-b2bf-4d5835e4aebd", DisplayName = "Guid with leading space(s)")]
        [DataRow("eb644f96-e978-4d49-b2bf-4d5835e4aebd ", DisplayName = "Guid with trailing space(s)")]
        [DataRow(" eb644f96-e978-4d49-b2bf-4d5835e4aebd ", DisplayName = "Guid with leading and trailing space(s)")]
        [DataRow("00000000-0000-0000-0000-000000000000", DisplayName = "Empty Guid")]
        [DataRow(null, DisplayName = "Guid null value")]
        [DataRow("", DisplayName = "Guid empty value")]
        [DataRow("   ", DisplayName = "Guid whitespaces value")]
        public void CreateTeamIdWithInvalidIdShouldFail(string id)
        {
            // Arrange
            string invalidId = id;

            // Act
            Action action = () => { TeamId.With(invalidId); };

            // Assert
            action.Should().Throw<InvalidIdentityException>();
        }
    }
}
