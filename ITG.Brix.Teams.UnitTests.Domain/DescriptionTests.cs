using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class DescriptionTests
    {
        [TestMethod]
        public void CreateDescriptionShouldSucceed()
        {
            // Arrange
            var text = "text";

            // Act
            var result = new Description(text);

            // Assert
            result.Should().Be(new Description(text));
        }

        [TestMethod]
        public void CreateDescritpitionShouldFailWhenTextIsNull()
        {
            // Arrange
            string text = null;

            // Act
            Action ctor = () => { new Description(text); };

            // Assert
            ctor.Should().Throw<DescriptionFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void CreateDescritpitionShouldFailWhenTextIsStringEmpty()
        {
            // Arrange
            var text = string.Empty;

            // Act
            Action ctor = () => { new Description(text); };

            // Assert
            ctor.Should().Throw<DescriptionFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void CreateDescritpitionShouldFailWhenTextIsWhitespace()
        {
            // Arrange
            var text = "   ";

            // Act
            Action ctor = () => { new Description(text); };

            // Assert
            ctor.Should().Throw<DescriptionFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void OperatorToStringShouldSucceed()
        {
            // Arrange
            var text = "text";
            var description = new Description(text);

            // Act
            string result = description;

            // Assert
            result.Should().Be(text);
        }

        [TestMethod]
        public void OperatorToDescriptionShouldReturnCorrectValue()
        {
            // Arrange
            var text = "text";

            // Act
            var result = (Description)text;

            // Assert
            result.Should().Be(new Description(text));
        }

        [TestMethod]
        public void OperatorToDescriptionShouldReturnNullFromStringNull()
        {
            // Arrange
            string text = null;

            // Act
            Description result = (Description)text;

            // Assert
            result.Should().Be(null);
        }

        [TestMethod]
        public void OperatorToDescriptionShouldReturnNullFromStringEmpty()
        {
            // Arrange
            var text = string.Empty;

            // Act
            Description result = (Description)text;

            // Assert
            result.Should().Be(null);
        }

        [TestMethod]
        public void OperatorToDescriptionShouldReturnNullFromStringWhitespace()
        {
            // Arrange
            var text = "   ";

            // Act
            Description result = (Description)text;

            // Assert
            result.Should().Be(null);
        }

        [TestMethod]
        public void DescriptionsAreEqualWhenTheyHaveSameTexts()
        {
            // Arrange
            var text = "text";

            var domain1 = new Description(text);
            var domain2 = new Description(text);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DescriptionsAreDifferentWhenTheyHaveDifferentTexts()
        {
            var text1 = "text-1";
            var text2 = "text-2";

            var domain1 = new Description(text1);
            var domain2 = new Description(text2);

            // Act
            var result = domain1.Equals(domain2);

            // Assert
            result.Should().BeFalse();
        }
    }
}

