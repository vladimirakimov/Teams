using FluentAssertions;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.Teams.Infrastructure.DataAccess.Repositories;
using ITG.Brix.Teams.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.Teams.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public class TeamReadRepositoryTests
    {
        [TestMethod]
        public void CtorShouldSucceed()
        {
#if DEBUG
            // Arrange
            var persistenceConfiguration = new PersistenceConfiguration("mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true");
            IPersistenceContext persistenceContext = new PersistenceContext(persistenceConfiguration);
            ITeamOdataProvider odataProvider = new Mock<ITeamOdataProvider>().Object;

            // Act
            Action ctor = () => { new TeamReadRepository(persistenceContext, odataProvider); };

            // Assert
            ctor.Should().NotThrow();
#endif
        }

        [TestMethod]
        public void CtorShouldFailWhenPersistenceContextNull()
        {
            // Arrange
            IPersistenceContext persistenceContext = null;
            ITeamOdataProvider odataProvider = new Mock<ITeamOdataProvider>().Object;

            // Act
            Action ctor = () => { new TeamReadRepository(persistenceContext, odataProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CtorShouldFailWhenTeamOdataProviderNull()
        {
            // Arrange
            var persistenceConfiguration = new PersistenceConfiguration("mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true");
            IPersistenceContext persistenceContext = new PersistenceContext(persistenceConfiguration);
            ITeamOdataProvider odataProvider = null;

            // Act
            Action ctor = () => { new TeamReadRepository(persistenceContext, odataProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
