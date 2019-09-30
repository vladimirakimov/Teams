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
    public class MemberClassMapTests
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
            var memberId = Guid.NewGuid();
            var memberClass = new LayoutClass() { Id = memberId };


            // Act
            var bson = memberClass.ToBson();
            var rehydrated = BsonSerializer.Deserialize<LayoutClass>(bson);

            // Assert
            rehydrated.Should().NotBeNull();
            rehydrated.Id.Should().Be(memberId);
        }
    }
}
