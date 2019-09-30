using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Requests.Models.Teams
{
    [TestClass]
    public class DeleteTeamRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var route = new DeleteTeamFromRoute();
            var query = new DeleteTeamFromQuery();
            var header = new DeleteTeamFromHeader();

            // Act
            var request = new DeleteTeamRequest(route, query, header);

            // Assert
            request.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRouteIsNull()
        {
            // Arrange
            DeleteTeamFromRoute route = null;
            var query = new DeleteTeamFromQuery();
            var header = new DeleteTeamFromHeader();

            // Act
            Action request = () => { new DeleteTeamRequest(route, query, header); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            var route = new DeleteTeamFromRoute();
            DeleteTeamFromQuery query = null;
            var header = new DeleteTeamFromHeader();

            // Act
            Action request = () => { new DeleteTeamRequest(route, query, header); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenHeaderIsNull()
        {
            // Arrange
            var route = new DeleteTeamFromRoute();
            var query = new DeleteTeamFromQuery();
            DeleteTeamFromHeader header = null;

            // Act
            Action request = () => { new DeleteTeamRequest(route, query, header); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }
    }
}
