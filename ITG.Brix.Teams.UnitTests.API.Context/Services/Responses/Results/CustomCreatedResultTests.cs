using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Responses.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Responses.Results
{
    [TestClass]
    public class CustomCreatedResultTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var location = $"/teams/{Guid.NewGuid()}";
            var eTag = "4324352435";

            // Act
            var obj = new CustomCreatedResult(location, eTag);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldSetETagValue()
        {
            // Arrange
            var location = $"/teams/{Guid.NewGuid()}";
            var eTag = "4324352435";

            // Act
            var obj = new CustomCreatedResult(location, eTag);

            // Assert
            obj.ETagValue.Should().Be(eTag);
        }

    }
}
