using FluentAssertions;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Infrastructure.Configurations
{
    [TestClass]
    public class PersistenceConfigurationTests
    {
        [TestMethod]
        public void CtorShouldSucceed()
        {
            // Arrange
            var connectionString = "connectionStringValue";

            // Act
            IPersistenceConfiguration result = new PersistenceConfiguration(connectionString);

            // Assert
            result.ConnectionString.Should().Be(connectionString);
            result.Database.Should().Be("Brix-Teams");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorShouldFail()
        {
            // Arrange
            string connectionString = null;

            // Act
            new PersistenceConfiguration(connectionString);
        }
    }
}
