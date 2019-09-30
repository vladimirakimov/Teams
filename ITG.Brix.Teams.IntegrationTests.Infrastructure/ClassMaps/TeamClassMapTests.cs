using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.ClassMaps
{
    [TestClass]
    public class TeamClassMapTests
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestMethod]
        public void TeamClassToAndFromBsonShouldSucceed()
        {
            // Arrange
            var teamId = TeamId.New;
            var teamName = new Name("Name");
            var teamImage = "Image.png";
            var teamDescription = (Description)"Team Description";
            var layoutId = Guid.NewGuid();
            var layout = new LayoutClass() { Id = layoutId };
            var filterContent = "filterContent";
            var team = new TeamClass()

            {
                Id = teamId.GetGuid(),
                Name = teamName,
                Description = teamDescription,
                Image = teamImage,
                Layout = layout,
                FilterContent = filterContent
            };

            // Act
            var bson = team.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TeamClass>(bson);

            // Assert
            rehydrated.Should().NotBeNull();
            rehydrated.Name.Should().Be(teamName);
            rehydrated.Image.Should().Be(teamImage);
            rehydrated.Description.Should().Be(teamDescription);
            rehydrated.Layout.Id.Should().Be(layoutId);
            rehydrated.FilterContent.Should().Be(filterContent);
        }
    }
}
