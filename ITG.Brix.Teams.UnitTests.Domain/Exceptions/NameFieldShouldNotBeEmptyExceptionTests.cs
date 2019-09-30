using FluentAssertions;
using ITG.Brix.Teams.Domain.Exceptions;
using ITG.Brix.Teams.Domain.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Domain.Exceptions
{
    [TestClass]
    public class NameFieldShouldNotBeEmptyExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = Consts.DomainExceptionMessage.NameFieldShouldNotBeEmpty;

            // Act
            var exception = new NameFieldShouldNotBeEmptyException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
