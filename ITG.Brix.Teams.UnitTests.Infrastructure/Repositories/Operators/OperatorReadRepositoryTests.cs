using FluentAssertions;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.Teams.Infrastructure.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.Teams.UnitTests.Infrastructure.Repositories.Operators
{
    [TestClass]
    public class OperatorReadRepositoryTests
    {
        [TestMethod]
        public void CtorShouldSucceed()
        {
#if DEBUG
            // Arrange
            var persistenceConfiguration = new PersistenceConfiguration("mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true");
            IPersistenceContext persistenceContext = new PersistenceContext(persistenceConfiguration);

            // Act
            Action ctor = () => { new OperatorReadRepository(persistenceContext); };

            // Assert
            ctor.Should().NotThrow();
#endif
        }

        [TestMethod]
        public void CtorShouldFailWhenPersistenceContextNull()
        {
            // Arrange
            IPersistenceContext persistenceContext = null;

            // Act
            Action ctor = () => { new OperatorReadRepository(persistenceContext); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();

        }
    }
}
