using FluentAssertions;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.ClassMaps
{
    [TestClass]
    public class LayoutClassMapTests
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestMethod]
        public void LayoutToAndFromBsonShouldSucceed()
        {
            // Arrange
            var layoutId = Guid.NewGuid();
            var layoutClass = new LayoutClass() { Id = layoutId };


            // Act
            var bson = layoutClass.ToBson();
            var rehydrated = BsonSerializer.Deserialize<LayoutClass>(bson);

            // Assert
            rehydrated.Should().NotBeNull();
            rehydrated.Id.Should().Be(layoutId);
        }
    }
}
