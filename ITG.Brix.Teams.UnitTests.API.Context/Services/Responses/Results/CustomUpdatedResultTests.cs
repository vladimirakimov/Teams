using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Responses.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Responses.Results
{
    [TestClass]
    public class CustomUpdatedResultTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var eTag = "353425234";

            // Act
            var obj = new CustomUpdatedResult(eTag);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldSetETagValue()
        {
            // Arrange
            var eTag = "34653363466";

            // Act
            var obj = new CustomUpdatedResult(eTag);

            // Assert
            obj.ETagValue.Should().Be(eTag);
        }
    }
}
