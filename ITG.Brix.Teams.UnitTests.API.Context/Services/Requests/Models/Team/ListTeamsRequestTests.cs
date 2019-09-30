using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Requests.Models.Teams
{
    [TestClass]
    public class ListTeamsRequestTests
    {
        [TestMethod]
        public void ConstructorShoulSucceed()
        {
            // Arrange
            var query = new ListTeamFromQuery();

            // Act
            var request = new ListTeamRequest(query);

            // Assert
            request.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange 
            ListTeamFromQuery query = null;

            // Act
            Action request = () => { new ListTeamRequest(query); };

            // Assert
            request.Should().Throw<ArgumentNullException>();
        }
    }
}
