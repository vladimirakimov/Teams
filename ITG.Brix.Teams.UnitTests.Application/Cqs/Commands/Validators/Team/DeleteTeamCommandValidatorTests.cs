using FluentAssertions;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Cqs.Commands.Validators;
using ITG.Brix.Teams.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class DeleteTeamCommandValidatorTests
    {
        private DeleteTeamCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new DeleteTeamCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new DeleteTeamCommand(id: Guid.NewGuid(), version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveTeamNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var command = new DeleteTeamCommand(id: Guid.Empty, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.TeamNotFound));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
