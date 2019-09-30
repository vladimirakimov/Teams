using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Constants;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.Teams.Infrastructure.DataAccess.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using ITG.Brix.Teams.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.Repositories.Operators
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperatorWriteRepositoryTests
    {
        private IOperatorWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.Operators);
            _repository = new OperatorWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var operatorId = Guid.NewGuid();
            var uniqueLogin = Guid.NewGuid().ToString();
            var newOperator = new Operator(operatorId, uniqueLogin, "anyFirstName", "anyLastName");

            // Act
            await _repository.CreateAsync(newOperator);

            // Assert
            var data = RepositoryHelper.ForOperator.GetOperators();
            data.Should().HaveCount(1);
            var result = data.First();


            result.Id.Should().Be(operatorId);
            result.Login.Should().Be(uniqueLogin);
            result.LastName.Should().Be("anyLastName");
            result.FirstName.Should().Be("anyFirstName");
        }

        [TestMethod]
        public void CreateWithAlreadyExistingLoginShouldFail()
        {
            // Arrange
            var operatorId = Guid.NewGuid();
            var notUniqueLogin = "existingLogin";
            var firstName = "anyFirstName";
            var lastName = "anyLastName";

            RepositoryHelper.ForOperator.CreateOperator(operatorId, notUniqueLogin, firstName, lastName);

            var otherId = Guid.NewGuid();
            var otherNotUniqueLogin = notUniqueLogin;

            var otherOperator = new Operator(otherId, otherNotUniqueLogin, firstName, lastName);

            // Act
            Action act = () => { _repository.CreateAsync(otherOperator).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public void CreateWithAlreadyExistingIdShouldFail()
        {
            // Arrange
            var operatorId = Guid.NewGuid();
            var uniqueLogin = Guid.NewGuid().ToString();
            var firstName = "anyFirstName";
            var lastName = "anyLastName";

            RepositoryHelper.ForOperator.CreateOperator(operatorId, uniqueLogin, firstName, lastName);



            var otherOperator = new Operator(operatorId, uniqueLogin, firstName, lastName);

            // Act
            Action act = () => { _repository.CreateAsync(otherOperator).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var operatorId = Guid.NewGuid();
            var uniqueLogin = Guid.NewGuid().ToString();
            var firstName = "anyFirstName";
            var lastName = "anyLastName";

            var newOperator = RepositoryHelper.ForOperator.CreateOperator(operatorId, uniqueLogin, firstName, lastName);

            var updatedOperator = new Operator(operatorId, uniqueLogin, "updatedFirstName", "updatedLastName");

            // Act
            await _repository.UpdateAsync(updatedOperator);

            // Assert
            var data = RepositoryHelper.ForOperator.GetOperators();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.FirstName.Should().Be("updatedFirstName");
            result.LastName.Should().Be("updatedLastName");
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingLoginShouldFail()
        {
            // Arrange
            var operatorId = Guid.NewGuid();
            var operatorId2 = Guid.NewGuid();
            var uniqueLogin = Guid.NewGuid().ToString();
            var uniqueLogin2 = Guid.NewGuid().ToString();
            var firstName = "anyFirstName";
            var lastName = "anyLastName";


            var newOperator = RepositoryHelper.ForOperator.CreateOperator(operatorId, uniqueLogin, firstName, lastName);
            var newOperator2 = RepositoryHelper.ForOperator.CreateOperator(operatorId2, uniqueLogin2, firstName, lastName);

            var updatedOperator = new Operator(operatorId2, uniqueLogin, "updatedFirstName", "updatedLastName");

            // Act
            Action act = () => { _repository.UpdateAsync(updatedOperator).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var operatorId = Guid.NewGuid();
            var uniqueLogin = Guid.NewGuid().ToString();
            var firstName = "anyFirstName";
            var lastName = "anyLastName";

            var newOperator = RepositoryHelper.ForOperator.CreateOperator(operatorId, uniqueLogin, firstName, lastName);

            // Act
            await _repository.DeleteAsync(operatorId, 0);

            // Assert
            var data = RepositoryHelper.ForOperator.GetOperators();
            data.Should().HaveCount(0);
        }
    }
}
