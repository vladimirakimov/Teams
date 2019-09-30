using FluentAssertions;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Cqs.Commands.Handlers;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteTeamCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteTeamCommandHandler(teamWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTeamWriteRepositoryIsNull()
        {
            // Arrange
            ITeamWriteRepository teamWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteTeamCommandHandler(teamWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.DeleteAsync(id, version)).Returns(Task.CompletedTask);
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var command = new DeleteTeamCommand(id, version);

            var handler = new DeleteTeamCommandHandler(teamWriteRepository);

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
            var version = 1;

            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.DeleteAsync(id, version)).Throws<EntityNotFoundDbException>();
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var command = new DeleteTeamCommand(id, version);

            var handler = new DeleteTeamCommandHandler(teamWriteRepository);

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
            var version = 1;

            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.DeleteAsync(id, version)).Throws<EntityVersionDbException>();
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var command = new DeleteTeamCommand(id, version);

            var handler = new DeleteTeamCommandHandler(teamWriteRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotMet.Name &&
                                                      x.Message == HandlerFailures.NotMet &&
                                                      x.Target == "version");
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.DeleteAsync(id, version)).Throws<SomeDatabaseSpecificException>();
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var command = new DeleteTeamCommand(id, version);

            var handler = new DeleteTeamCommandHandler(teamWriteRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.DeleteTeamFailure);
        }


        public class SomeDatabaseSpecificException : Exception { }
    }
}
