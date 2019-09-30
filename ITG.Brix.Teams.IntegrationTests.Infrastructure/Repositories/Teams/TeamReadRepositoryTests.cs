using FluentAssertions;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Constants;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.Teams.Infrastructure.DataAccess.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using ITG.Brix.Teams.Infrastructure.Providers;
using ITG.Brix.Teams.Infrastructure.Providers.Impl;
using ITG.Brix.Teams.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class TeamReadRepositoryTests
    {
        private ITeamReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.Teams);
            ITeamOdataProvider odataProvider = new TeamOdataProvider();
            _repository = new TeamReadRepository(new Teams.Infrastructure.DataAccess.Configurations.Impl.PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider);
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            RepositoryHelper.ForTeam.CreateTeam(id, name);

            // Act
            var result = await _repository.GetAsync(id.GetGuid());

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
        }

        [TestMethod]
        public void GetByNonExistentIdShouldThrowEntityNotFoundDbException()
        {
            // Arrange
            var nonExistentTeamId = Guid.NewGuid();

            // Act
            Func<Task> call = async () => { await _repository.GetAsync(nonExistentTeamId); };

            // Assert
            call.Should().Throw<EntityNotFoundDbException>();
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            RepositoryHelper.ForTeam.CreateTeam(TeamId.New, new Name("Team Name 1"));
            RepositoryHelper.ForTeam.CreateTeam(TeamId.New, new Name("Team Name 2"));
            RepositoryHelper.ForTeam.CreateTeam(TeamId.New, new Name("Team Name 3"));

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredByMemberRecord()
        {
            // Arrange
            var teamName1 = new Name("Team Name 1");
            var teamName2 = new Name("Team Name 2");
            var teamName3 = new Name("Team Name 3");
            var memberId1 = Guid.NewGuid();
            var memberId2 = Guid.NewGuid();
            RepositoryHelper.ForTeam.CreateTeam(TeamId.New, teamName1);
            RepositoryHelper.ForTeam.CreateTeamWithMembers(TeamId.New, teamName2, new List<Guid> { memberId1, memberId2 });
            RepositoryHelper.ForTeam.CreateTeam(TeamId.New, teamName3);

            var filter = string.Format("members/id eq '{0}'", memberId1);

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(1);
            result.First().Name.Should().Be(teamName2);
        }

    }
}
