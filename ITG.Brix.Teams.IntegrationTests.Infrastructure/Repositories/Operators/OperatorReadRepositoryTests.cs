using FluentAssertions;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Constants;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.Teams.Infrastructure.DataAccess.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using ITG.Brix.Teams.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.Repositories.Operators
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperatorReadRepositoryTests
    {
        private IOperatorReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.Operators);
            _repository = new OperatorReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var login = "login";
            var firstName = "firstName";
            var lastName = "lastName";

            RepositoryHelper.ForOperator.CreateOperator(id, login, firstName, lastName);

            // Act
            var result = await _repository.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Login.Should().Be(login);
            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
        }

        [TestMethod]
        public void GetByNonExistentIdShouldThrowEntityNotFoundDbException()
        {
            // Arrange
            var nonExistentOperatorId = Guid.NewGuid();

            // Act
            Func<Task> call = async () => { await _repository.GetAsync(nonExistentOperatorId); };

            // Assert
            call.Should().Throw<EntityNotFoundDbException>();
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            RepositoryHelper.ForOperator.CreateOperator(Guid.NewGuid(), "login-1", "firstName", "lastName");
            RepositoryHelper.ForOperator.CreateOperator(Guid.NewGuid(), "login-2", "firstName", "lastName");
            RepositoryHelper.ForOperator.CreateOperator(Guid.NewGuid(), "login-3", "firstName", "lastName");

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}
