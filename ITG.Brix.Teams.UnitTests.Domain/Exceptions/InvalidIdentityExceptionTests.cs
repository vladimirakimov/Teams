using FluentAssertions;
using ITG.Brix.Teams.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Domain.Exceptions
{
    [TestClass]
    public class InvalidIdentityExceptionTests
    {
        [TestMethod]
        public void ShouldSetExceptionMessage()
        {
            // Arrange
            var expectedMessage = "Exception message";

            // Act
            var exception = new InvalidIdentityException(expectedMessage);

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
