using FluentAssertions;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Cqs.Commands.Handlers;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using ITG.Brix.Teams.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class UpdateTeamCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }


        [TestMethod]
        public void ConstructorShouldFailWhenTeamWriteRepositoryIsNull()
        {
            // Arrange
            ITeamWriteRepository teamWriteRepository = null;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTeamReadRepositoryIsNull()
        {
            // Arrange
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;
            ITeamReadRepository teamReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "Yes";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var team = new Team(TeamId.With(id), new Name(name));
            team.SetVersion(version);


            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Team>())).Returns(Task.CompletedTask);
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(team));
            var teamReadRepository = teamReadRepositoryMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            var handler = new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "Yes";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;


            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Team>())).Returns(Task.CompletedTask);
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var teamReadRepository = teamReadRepositoryMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            var handler = new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);



            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name &&
                                                      x.Message == HandlerFailures.TeamNotFound &&
                                                      x.Target == "id");
        }


        [TestMethod]
        public async Task HandleShouldReturnFailWhenOutdatedVersion()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "Yes";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var team = new Team(TeamId.With(id), new Name(name));
            team.SetVersion(0);


            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Team>())).Returns(Task.CompletedTask);
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(team));
            var teamReadRepository = teamReadRepositoryMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            var handler = new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotMet.Name &&
                                                      x.Message == HandlerFailures.NotMet &&
                                                      x.Target == "version");
        }

        [TestMethod]
        public async Task HandleShouldFailWhenRecordWithSameNameAlreadyExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "Yes";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var team = new Team(TeamId.With(id), new Name(name));
            team.SetVersion(version);


            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Team>())).Throws<UniqueKeyException>();
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(team));
            var teamReadRepository = teamReadRepositoryMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            var handler = new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.Conflict.Name &&
                                                      x.Message == HandlerFailures.ConflictTeam &&
                                                      x.Target == "name");
        }


        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "Yes";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;

            var team = new Team(TeamId.With(id), new Name(name));
            team.SetVersion(version);


            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Team>())).Throws<SomeDatabaseSpecificException>();
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(team));
            var teamReadRepository = teamReadRepositoryMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var command = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            var handler = new UpdateTeamCommandHandler(teamWriteRepository, teamReadRepository, versionProvider);


            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.UpdateTeamFailure);
        }


        public class SomeDatabaseSpecificException : Exception { }
    }
}
