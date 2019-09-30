using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class NameTests
    {
        [TestMethod]
        public void CreateNameShouldSucceed()
        {
            // Arrange
            var text = "text";

            // Act
            var result = new Name(text);

            // Assert
            result.Should().Be(new Name(text));
        }

        [TestMethod]
        public void CreateNameShouldFailWhenTextIsNull()
        {
            // Arrange
            string text = null;

            // Act
            Action ctor = () => { new Name(text); };

            // Assert
            ctor.Should().Throw<NameFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void CreateNameShouldFailWhenNTextIsStringEmpty()
        {
            // Arrange
            var text = string.Empty;

            // Act
            Action ctor = () => { new Name(text); };

            // Assert
            ctor.Should().Throw<NameFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void CreateNameShouldFailWhenTextIsWhitespace()
        {
            // Arrange
            var text = "   ";

            // Act
            Action ctor = () => { new Name(text); };

            // Assert
            ctor.Should().Throw<NameFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void OperatorToStringShouldSucceed()
        {
            // Arrange
            var text = "text";
            var name = new Name(text);

            // Act
            string result = name;

            // Assert
            result.Should().Be(text);
        }

        [TestMethod]
        public void OperatorToNameShouldReturnCorrectValue()
        {
            // Arrange
            var text = "text";

            // Act
            var result = (Name)text;

            // Assert
            result.Should().Be(new Name(text));
        }

        [TestMethod]
        public void OperatorToNameShouldReturnNullFromStringNull()
        {
            // Arrange
            string text = null;

            // Act
            Name result = (Name)text;

            // Assert
            result.Should().Be(null);
        }

        [TestMethod]
        public void OperatorToNameShouldReturnNullFromStringEmpty()
        {
            // Arrange
            var text = string.Empty;

            // Act
            Name result = (Name)text;

            // Assert
            result.Should().Be(null);
        }

        [TestMethod]
        public void OperatorToNameShouldReturnNullFromStringWhitespace()
        {
            // Arrange
            var text = "   ";

            // Act
            Name result = (Name)text;

            // Assert
            result.Should().Be(null);
        }

        [TestMethod]
        public void NamesAreEqualWhenTheyHaveSameTexts()
        {
            // Arrange
            var text = "text";

            var domain1 = new Name(text);
            var domain2 = new Name(text);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void NamesAreDifferentWhenTheyHaveDifferentTexts()
        {
            var text1 = "text-1";
            var text2 = "text-2";

            var domain1 = new Name(text1);
            var domain2 = new Name(text2);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

