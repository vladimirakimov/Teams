using FluentAssertions;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Cqs.Commands.Validators;
using ITG.Brix.Teams.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class UpdateTeamCommandValidatorTests
    {
        private UpdateTeamCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateTeamCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "No";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveDriverWaitDoesNotExistFailureWhenDriverWaitIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "someString";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
               validationResult.Errors.Any(
                   a => a.PropertyName.Equals("DriverWait") && a.ErrorMessage.Contains(ValidationFailures.TeamDriverWaitWrongValue));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDriverWaitDoesNotExistFailureWhenDriverWaitIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            string driverWait = null;
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
               validationResult.Errors.Any(
                   a => a.PropertyName.Equals("DriverWait") && a.ErrorMessage.Contains(ValidationFailures.TeamDriverWaitWrongValue));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveTeamNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "driverWait";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.TeamNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveTeamNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "driverWait";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

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
            var id = Guid.NewGuid();
            var name = string.Empty;
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "driverWait";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

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
            var id = Guid.NewGuid();
            var name = "   ";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "driverWait";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

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
