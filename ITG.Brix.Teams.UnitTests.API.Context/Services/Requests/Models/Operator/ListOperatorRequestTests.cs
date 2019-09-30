using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Requests.Models.Operators
{
    [TestClass]
    public class ListOperatorRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var query = new ListOperatorFromQuery();

            // Act
            var request = new ListOperatorRequest(query);

            // Assert
            request.Should().NotBeNull();
        }


        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            ListOperatorFromQuery query = null;

            // Act
            Action request = () => { new ListOperatorRequest(query); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }
    }
}
