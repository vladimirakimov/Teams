using FluentAssertions;
using ITG.Brix.Teams.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class OperatorTests
    {
        [TestMethod]
        public void ConstructorShouldWhenAllParametersAreCorrect()
        {
            // Arrange 
            var id = Guid.NewGuid();
            var login = "KTNBEL\test";
            var firstName = "test";
            var lastName = "testov";

            // Act
            var oper = new Operator(id, login, firstName, lastName);

            // Assert
            oper.Login.Should().Be(login);
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIdIsDefaultGuid()
        {
            // Arrange 
            var id = Guid.Empty;
            var login = "KTNBEL\test";
            var firstName = "test";
            var lastName = "testov";

            // Act
            Action ctor = () => { new Operator(id, login, firstName, lastName); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLoginIsNull()
        {
            // Arrange 
            var id = Guid.NewGuid();
            string login = null;
            var firstName = "test";
            var lastName = "testov";

            // Act
            Action ctor = () => { new Operator(id, login, firstName, lastName); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLoginIsEmpty()
        {
            // Arrange 
            var id = Guid.NewGuid();
            string login = string.Empty;
            var firstName = "test";
            var lastName = "testov";

            // Act
            Action ctor = () => { new Operator(id, login, firstName, lastName); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFirstNameIsNull()
        {
            // Arrange 
            var id = Guid.NewGuid();
            string login = "Test";
            string firstName = null;
            var lastName = "testov";

            // Act
            Action ctor = () => { new Operator(id, login, firstName, lastName); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFirstNameIsEmpty()
        {
            // Arrange 
            var id = Guid.NewGuid();
            string login = "Test";
            string firstName = string.Empty;
            var lastName = "testov";

            // Act
            Action ctor = () => { new Operator(id, login, firstName, lastName); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLastNameIsNull()
        {
            // Arrange 
            var id = Guid.NewGuid();
            var login = "KTNBEL\test";
            var firstName = "test";
            string lastName = null;

            // Act
            Action ctor = () => { new Operator(id, login, firstName, lastName); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLastNameIsEmpty()
        {
            // Arrange 
            var id = Guid.NewGuid();
            var login = "KTNBEL\test";
            var firstName = "test";
            string lastName = string.Empty;

            // Act
            Action ctor = () => { new Operator(id, login, firstName, lastName); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }
    }
}
