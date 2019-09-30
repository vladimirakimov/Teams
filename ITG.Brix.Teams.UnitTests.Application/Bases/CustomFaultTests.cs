using FluentAssertions;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Application.Bases
{
    [TestClass]
    public class CustomFaultTests
    {
        [TestMethod]
        public void ShouldHaveCustomErrorType()
        {
            // Arrange
            var fault = new CustomFault();

            // Act
            var type = fault.Type;

            // Assert
            type.Should().Be(ErrorType.CustomError);
        }
    }
}
