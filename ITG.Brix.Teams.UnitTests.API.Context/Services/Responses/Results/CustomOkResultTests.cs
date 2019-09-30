using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Responses.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Responses.Results
{
    [TestClass]
    public class CustomOkResultTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var location = $"/teams/{Guid.NewGuid()}";
            var eTag = "43536533454";

            // Act
            var obj = new CustomOkResult(location, eTag);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldSetETagVAlue()
        {
            // Arrange
            var location = $"/teams/{Guid.NewGuid()}";
            var eTag = "43536533454";

            // Act 
            var obj = new CustomOkResult(location, eTag);

            // Assert
            obj.ETagValue.Should().Be(eTag);
        }
    }
}
