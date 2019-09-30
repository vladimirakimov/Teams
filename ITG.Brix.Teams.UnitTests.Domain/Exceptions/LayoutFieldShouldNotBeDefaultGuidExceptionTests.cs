using FluentAssertions;
using ITG.Brix.Teams.Domain.Exceptions;
using ITG.Brix.Teams.Domain.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Domain.Exceptions
{
    [TestClass]
    public class LayoutFieldShouldNotBeDefaultGuidExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = Consts.DomainExceptionMessage.LayoutFieldShouldNotBeDefaultGuid;

            // Act
            var exception = new LayoutFieldShouldNotBeDefaultGuidException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
