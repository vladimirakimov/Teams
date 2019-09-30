using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Requests.Models.Teams
{
    [TestClass]
    public class CreateTeamRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var query = new CreateTeamFromQuery();
            var body = new CreateTeamFromBody();

            // Act
            var request = new CreateTeamRequest(query, body);

            // Assert
            request.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRequestIsNull()
        {
            // Arrange 
            CreateTeamFromQuery query = null;
            var body = new CreateTeamFromBody();

            // Act
            Action request = () => { new CreateTeamRequest(query, body); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBodyIsNull()
        {
            // Arrange
            var query = new CreateTeamFromQuery();
            CreateTeamFromBody body = null;

            // Act
            Action request = () => { new CreateTeamRequest(query, body); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }
    }
}
