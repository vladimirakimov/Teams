using FluentAssertions;
using ITG.Brix.Teams.Domain.Exceptions;
using ITG.Brix.Teams.Domain.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Domain.Exceptions
{
    [TestClass]
    public class MemberFieldShouldNotBeDefaultGuidExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = Consts.DomainExceptionMessage.MemberFieldShouldNotBeDefaultGuid;

            // Act
            var exception = new MemberFieldShouldNotBeDefaultGuidException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
