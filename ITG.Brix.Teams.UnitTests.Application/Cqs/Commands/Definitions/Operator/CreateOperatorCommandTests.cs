using FluentAssertions;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Definitions
{
    [TestClass]
    public class CreateOperatorCommandTests
    {
        [TestMethod]
        public void CtorShouldFillProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "login";
            var firstName = "firstName";
            var lastName = "lastName";

            // Act
            var result = new CreateOperatorCommand(id, login, firstName, lastName);

            // Assert
            result.Id.Should().Be(id);
            result.Login.Should().Be(login);
            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
        }
    }
}
