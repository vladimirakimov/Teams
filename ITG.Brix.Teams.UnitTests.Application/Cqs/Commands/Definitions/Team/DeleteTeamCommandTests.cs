using FluentAssertions;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Definitions
{
    [TestClass]
    public class DeleteTeamCommandTests
    {
        [TestMethod]
        public void CtorShouldFillProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            // Act
            var result = new DeleteTeamCommand(id, version);

            // Assert
            result.Id.Should().Be(id);
            result.Version.Should().Be(version);
        }
    }
}
