using FluentAssertions;
using ITG.Brix.Teams.Domain.Exceptions;
using ITG.Brix.Teams.Domain.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Domain.Exceptions
{
    [TestClass]
    public class DescriptionFieldShouldNotBeEmptyExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = Consts.DomainExceptionMessage.DescriptionFieldShouldNotBeEmpty;

            // Act
            var exception = new DescriptionFieldShouldNotBeEmptyException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
