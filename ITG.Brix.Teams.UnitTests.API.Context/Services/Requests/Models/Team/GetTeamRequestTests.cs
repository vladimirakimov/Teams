using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Requests.Models.Teams
{
    [TestClass]
    public class GetTeamRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var route = new GetTeamFromRoute();
            var query = new GetTeamFromQuery();

            // Act
            var request = new GetTeamRequest(route, query);

            // Assert
            request.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRouteIsNull()
        {
            // Arrange
            GetTeamFromRoute route = null;
            var query = new GetTeamFromQuery();

            // Act
            Action request = () => { new GetTeamRequest(route, query); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRequestIsNull()
        {
            // Arrange
            var route = new GetTeamFromRoute();
            GetTeamFromQuery query = null;

            // Act
            Action request = () => { new GetTeamRequest(route, query); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }
    }
}
