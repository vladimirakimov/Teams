using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Requests.Models.Teams
{
    [TestClass]
    public class UpdateTeamRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var header = new UpdateTeamFromHeader();
            var query = new UpdateTeamFromQuery();
            var route = new UpdateTeamFromRoute();
            var body = new UpdateTeamFromBody();

            // Act
            var request = new UpdateTeamRequest(header, query, route, body);

            // Assert
            request.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorWithNullHeaderShouldFail()
        {
            // Arrange
            UpdateTeamFromHeader header = null;
            var query = new UpdateTeamFromQuery();
            var route = new UpdateTeamFromRoute();
            var body = new UpdateTeamFromBody();

            // Act
            Action request = () => { new UpdateTeamRequest(header, query, route, body); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            var header = new UpdateTeamFromHeader();
            UpdateTeamFromQuery query = null;
            var route = new UpdateTeamFromRoute();
            var body = new UpdateTeamFromBody();

            // Act
            Action request = () => { new UpdateTeamRequest(header, query, route, body); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRouteIsNull()
        {
            // Arrange
            var header = new UpdateTeamFromHeader();
            var query = new UpdateTeamFromQuery();
            UpdateTeamFromRoute route = null;
            var body = new UpdateTeamFromBody();

            // Act
            Action request = () => { new UpdateTeamRequest(header, query, route, body); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBodyIsNull()
        {
            // Arrange
            var header = new UpdateTeamFromHeader();
            var query = new UpdateTeamFromQuery();
            var route = new UpdateTeamFromRoute();
            UpdateTeamFromBody body = null;

            // Act
            Action request = () => { new UpdateTeamRequest(header, query, route, body); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }
    }
}
