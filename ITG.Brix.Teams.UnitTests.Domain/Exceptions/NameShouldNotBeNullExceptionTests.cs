using FluentAssertions;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Domain.Exceptions
{
    [TestClass]
    public class NameShouldNotBeNullExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = "Name shouldn't be null.";

            // Act
            var exception = new NameShouldNotBeNullException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
