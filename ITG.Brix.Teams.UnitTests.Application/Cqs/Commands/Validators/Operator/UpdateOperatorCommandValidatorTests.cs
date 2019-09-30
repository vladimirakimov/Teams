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
    public class UpdateOperatorCommandValidatorTests
    {
        private UpdateOperatorCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateOperatorCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "Login";
            var firstName = "UpdatedFirstName";
            var lastName = "UpdatedLastName";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveOperatorNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            Guid id = Guid.Empty;
            var login = "Login";
            var firstName = "UpdatedFirstName";
            var lastName = "UpdatedLastName";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.OperatorNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorLoginMandatoryValidationFailureWhenLoginIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string login = null;
            var firstName = "UpdatedFirstName";
            var lastName = "UpdatedLastName";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Login") && a.ErrorMessage.Contains(ValidationFailures.OperatorLoginMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorLoginMandatoryValidationFailureWhenLoginIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = string.Empty;
            var firstName = "UpdatedFirstName";
            var lastName = "UpdatedLastName";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Login") && a.ErrorMessage.Contains(ValidationFailures.OperatorLoginMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorLoginMandatoryValidationFailureWhenLoginIsWhiteSpace()
        {
            var id = Guid.NewGuid();
            var login = "   ";
            var firstName = "UpdatedFirstName";
            var lastName = "UpdatedLastName";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Login") && a.ErrorMessage.Contains(ValidationFailures.OperatorLoginMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorFirstNameValidationFailureWhenValueIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "login";
            string firstName = null;
            var lastName = "UpdatedLastName";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("FirstName") && a.ErrorMessage.Contains(ValidationFailures.OperatorFirstNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorFirstNameValidationFailureWhenValueIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "login";
            var firstName = string.Empty;
            var lastName = "UpdatedLastName";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("FirstName") && a.ErrorMessage.Contains(ValidationFailures.OperatorFirstNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorFirstNameValidationFailureWhenValueIsWhiteSpace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "login";
            var firstName = "   ";
            var lastName = "UpdatedLastName";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("FirstName") && a.ErrorMessage.Contains(ValidationFailures.OperatorFirstNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorLastNameValidationFailureWhenValueIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "login";
            var firstName = "UpdatedFirstName";
            string lastName = null;

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("LastName") && a.ErrorMessage.Contains(ValidationFailures.OperatorLastNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorLastNameValidationFailureWhenValueIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "login";
            var firstName = "UpdatedFirstName";
            var lastName = string.Empty;

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("LastName") && a.ErrorMessage.Contains(ValidationFailures.OperatorLastNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperatorLastNameValidationFailureWhenValueIsWhiteSpace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "login";
            var firstName = "UpdatedFirstName";
            var lastName = "   ";

            var command = new UpdateOperatorCommand(id, login, firstName, lastName);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("LastName") && a.ErrorMessage.Contains(ValidationFailures.OperatorLastNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

    }
}
