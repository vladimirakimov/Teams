using FluentAssertions;
using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain.Bases
{
    [TestClass]
    public class IdentityTests
    {
        [TestMethod]
        public void GuidToAndFromIdentifierShouldSucceed()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var entityId = EntityId.With(guid);
            var result = entityId.GetGuid();

            // Assert
            result.Should().Be(guid);
        }

        [TestMethod]
        public void IdentityNewValueShouldContainLowerCasedValue()
        {
            // Act
            var entityId = EntityId.New;

            // Assert
            entityId.Value.Should().Be(entityId.Value.ToLowerInvariant());
        }

        [TestMethod]
        public void IdentityNewValidShouldBeValid()
        {
            // Arrange
            var entityId = EntityId.New;

            // Act
            var result = EntityId.IsValid(entityId.Value);

            // Assert
            result.Should().BeTrue();
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
        public void CreateIdentityWithInvalidIdShouldFail(string id)
        {
            // Arrange
            string invalidId = id;

            // Act
            Action action = () => { EntityId.With(invalidId); };

            // Assert
            action.Should().Throw<InvalidIdentityException>();
        }

    }

    public class EntityId : Identity<EntityId>
    {
        public EntityId(string value) : base(value)
        {
        }
    }
}
