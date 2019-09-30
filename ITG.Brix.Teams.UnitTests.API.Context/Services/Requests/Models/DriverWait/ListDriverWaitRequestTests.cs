using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Requests.Models.DriverWait
{
    [TestClass]
    public class ListDriverWaitRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var query = new ListDriverWaitFromQuery();

            // Act
            var request = new ListDriverWaitRequest(query);

            // Assert
            request.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            ListDriverWaitFromQuery query = null;

            // Act
            Action request = () => { new ListDriverWaitRequest(query); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }
    }
}
