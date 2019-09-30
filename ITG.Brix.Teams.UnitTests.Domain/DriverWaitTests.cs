using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Domain
{
    [TestClass]
    public class DriverWaitTests
    {
        [TestMethod]
        public void ParseShouldReturnCorrectValue()
        {
            // Arrange
            string name = DriverWait.Yes.ToString();

            // Act
            Action action = () => { DriverWait.Parse(name); };

            // Assert
            action.Should().Throw<InvalidEnumerationException>();
        }

        [TestMethod]
        public void ParseShouldFailWhenInvalidEnumerationValue()
        {
            // Arrange
            string name = "some invalid enumeration string value";

            // Act
            Action action = () => { DriverWait.Parse(name); };

            // Assert
            action.Should().Throw<InvalidEnumerationException>();
        }

    }
}

