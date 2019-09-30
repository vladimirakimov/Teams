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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class TeamWriteRepositoryTests
    {
        private ITeamWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.Teams);
            _repository = new TeamWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");
            var team = new Team(id, name);

            var image = "image";
            team.SetImage(image);

            var description = (Description)"description";
            team.SetDescription(description);

            var layout = new Layout(Guid.NewGuid());
            team.SetLayout(layout);

            var memberId = Guid.NewGuid();
            var member = new Member(memberId);
            team.AddMember(member);

            // Act
            await _repository.CreateAsync(team);

            // Assert
            var data = RepositoryHelper.ForTeam.GetTeams();
            data.Should().HaveCount(1);
            var result = data.First();

            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
            result.Image.Should().Be(image);
            result.Description.Should().Be(description);
            result.DriverWait.Should().Be(DriverWait.Unspecified);
            result.Layout.Should().Be(layout);

            result.Members.Should().NotBeNull();
            result.Members.Should().HaveCount(1);
            result.Members.First().Should().Be(member);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            RepositoryHelper.ForTeam.CreateTeam(id, name);

            var otherId = TeamId.New;
            var otherName = name;

            var layout = new Team(otherId, otherName);

            // Act
            Action act = () => { _repository.CreateAsync(layout).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");


            var team = RepositoryHelper.ForTeam.CreateTeam(id, name);

            var otherName = new Name("otherName");
            team.ChangeName(otherName);

            // Act
            await _repository.UpdateAsync(team);

            // Assert
            var data = RepositoryHelper.ForTeam.GetTeams();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Name.Should().Be(otherName);
        }

        [TestMethod]
        public async Task UpdateWithMembersShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");
            var oper1Id = Guid.NewGuid();
            var oper2Id = Guid.NewGuid();
            var uniqueLogin = Guid.NewGuid().ToString();
            var otherUniqueLogin = Guid.NewGuid().ToString();
            var memberId = RepositoryHelper.ForOperator.CreateOperator(oper1Id, uniqueLogin, "anyFirstName", "anyLastName");
            var updatedMemberId = RepositoryHelper.ForOperator.CreateOperator(oper2Id, otherUniqueLogin, "anyFirstName", "anyLastName");
            var membersList = new List<Guid>();
            membersList.Add(memberId.Id);
            var team = RepositoryHelper.ForTeam.CreateTeamWithMembers(id, name, membersList);
            var otherName = new Name("otherName");
            team.ChangeName(otherName);
            team.AddMember(updatedMemberId.Id);
            // Act
            await _repository.UpdateAsync(team);

            // Assert
            var data = RepositoryHelper.ForTeam.GetTeams();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Name.Should().Be(otherName);
            result.Members.Count.Should().Be(2);
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");


            RepositoryHelper.ForTeam.CreateTeam(id, name);


            var otherId = TeamId.New;
            var otherName = new Name("nameTwo");

            var other = RepositoryHelper.ForTeam.CreateTeam(otherId, otherName);

            other.ChangeName(name);

            // Act
            Action act = () => { _repository.UpdateAsync(other).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var id = TeamId.New;
            var name = new Name("name");

            RepositoryHelper.ForTeam.CreateTeam(id, name);

            // Act
            await _repository.DeleteAsync(id.GetGuid(), 0);

            // Assert
            var data = RepositoryHelper.ForTeam.GetTeams();
            data.Should().HaveCount(0);
        }

        [TestMethod]
        public async Task UpdateTeamsWithDeletedMembersShouldSucceed()
        {
            // Arrange

            var memberId = Guid.NewGuid();
            var member2Id = Guid.NewGuid();
            var team1 = new Team(new TeamId(Guid.NewGuid().ToString()), new Name("anyName1"));
            var team2 = new Team(new TeamId(Guid.NewGuid().ToString()), new Name("anyName2"));
            var member = new Member(memberId);
            var member2 = new Member(member2Id);
            team1.AddMember(member);
            team1.AddMember(member2);
            team2.AddMember(member);
            team2.AddMember(member2);

            await _repository.CreateAsync(team1);
            await _repository.CreateAsync(team2);

            var teamList = RepositoryHelper.ForTeam.GetTeams().ToList();

            // Act
            foreach (var item in teamList)
            {
                item.RemoveMember(memberId);
                await _repository.UpdateAsync(item);
            }

            // Assert
            var updatedTeamList = RepositoryHelper.ForTeam.GetTeams().ToList();
            updatedTeamList.Count(x => x.Members.Any(y => y == member)).Should().Be(0);
            updatedTeamList.Count(x => (x.Members.Count > 0)).Should().Be(2);
            updatedTeamList.Should().NotContain(x => (x.Members.Contains(member)));
            updatedTeamList.Should().OnlyContain(x => (x.Members.Contains(member2)));
        }
    }
}
