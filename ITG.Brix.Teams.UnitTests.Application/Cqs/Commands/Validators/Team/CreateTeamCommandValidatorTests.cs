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
    public class CreateTeamCommandValidatorTests
    {
        private CreateTeamCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateTeamCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = Guid.NewGuid().ToString();

            var command = new CreateTeamCommand(name, image, description, layout);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveTeamNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;
            var image = "image";
            var description = "description";
            var layout = Guid.NewGuid().ToString();

            var command = new CreateTeamCommand(name, image, description, layout);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.TeamNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveTeamNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            var image = "image";
            var description = "description";
            var layout = Guid.NewGuid().ToString();

            var command = new CreateTeamCommand(name, image, description, layout);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.TeamNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveTeamNameMandatoryValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var name = "   ";
            var image = "image";
            var description = "description";
            var layout = Guid.NewGuid().ToString();

            var command = new CreateTeamCommand(name, image, description, layout);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.TeamNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
