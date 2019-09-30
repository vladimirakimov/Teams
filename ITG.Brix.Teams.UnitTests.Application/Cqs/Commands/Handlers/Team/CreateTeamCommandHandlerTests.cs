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
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class CreateTeamCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateTeamCommandHandler(teamWriteRepository, identifierProvider, versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTeamWriteRepositoryIsNull()
        {
            // Arrange
            ITeamWriteRepository teamWriteRepository = null;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateTeamCommandHandler(teamWriteRepository, identifierProvider, versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIdentifierProviderIsNull()
        {
            // Arrange
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;
            IIdentifierProvider identifierProvider = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateTeamCommandHandler(teamWriteRepository, identifierProvider, versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new CreateTeamCommandHandler(teamWriteRepository, identifierProvider, versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";

            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Team>())).Returns(Task.CompletedTask);
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var identifierProviderMock = new Mock<IIdentifierProvider>();
            identifierProviderMock.Setup(x => x.Generate()).Returns(id);
            var identifierProvider = identifierProviderMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var command = new CreateTeamCommand(name, image, description, layout);

            var handler = new CreateTeamCommandHandler(teamWriteRepository, identifierProvider, versionProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<Guid>));
        }

        [TestMethod]
        public async Task HandleShouldFailWhenRecordWithSameNameAlreadyExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";

            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Team>())).Throws<UniqueKeyException>();
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var identifierProviderMock = new Mock<IIdentifierProvider>();
            identifierProviderMock.Setup(x => x.Generate()).Returns(id);
            var identifierProvider = identifierProviderMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var command = new CreateTeamCommand(name, image, description, layout);

            var handler = new CreateTeamCommandHandler(teamWriteRepository, identifierProvider, versionProvider);

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
            var version = 1;

            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";

            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Team>())).Throws<SomeDatabaseSpecificException>();
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var identifierProviderMock = new Mock<IIdentifierProvider>();
            identifierProviderMock.Setup(x => x.Generate()).Returns(id);
            var identifierProvider = identifierProviderMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var command = new CreateTeamCommand(name, image, description, layout);

            var handler = new CreateTeamCommandHandler(teamWriteRepository, identifierProvider, versionProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.CreateTeamFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
