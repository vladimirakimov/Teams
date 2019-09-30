using FluentAssertions;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Cqs.Commands.Handlers;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.operators.UnitTests.Application.Cqs.Commands.Handlers.Operator
{
    [TestClass]
    public class DeleteOperatorCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var operatorWriteRepository = new Mock<IOperatorWriteRepository>().Object;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteOperatorCommandHandler(teamReadRepository, teamWriteRepository, operatorWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTeamWriteRepositoryIsNull()
        {
            // Arrange
            ITeamWriteRepository teamWriteRepository = null;
            var operatorWriteRepository = new Mock<IOperatorWriteRepository>().Object;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;

            // Act
            Action ctor = () => { new DeleteOperatorCommandHandler(teamReadRepository, teamWriteRepository, operatorWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTeamReadRepositoryIsNull()
        {
            // Arrange
            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;
            var operatorWriteRepository = new Mock<IOperatorWriteRepository>().Object;
            ITeamReadRepository teamReadRepository = null;

            // Act
            Action ctor = () => { new DeleteOperatorCommandHandler(teamReadRepository, teamWriteRepository, operatorWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            var operatorWriteRepository = new Mock<IOperatorWriteRepository>().Object;

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(new List<Team>() as IEnumerable<Team>));
            var teamReadRepository = teamReadRepositoryMock.Object;

            var teamWriteRepositoryMock = new Mock<ITeamWriteRepository>();
            teamWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Team>())).Returns(Task.CompletedTask);
            var teamWriteRepository = teamWriteRepositoryMock.Object;

            var command = new DeleteOperatorCommand(id);

            var handler = new DeleteOperatorCommandHandler(teamReadRepository, teamWriteRepository, operatorWriteRepository);

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

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(new List<Team>() as IEnumerable<Team>));
            var teamReadRepository = teamReadRepositoryMock.Object;

            var operatorWriteRepositoryMock = new Mock<IOperatorWriteRepository>();
            operatorWriteRepositoryMock.Setup(x => x.DeleteAsync(id, 0)).Throws<EntityNotFoundDbException>();
            var operatorWriteRepository = operatorWriteRepositoryMock.Object;

            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;

            var command = new DeleteOperatorCommand(id);

            var handler = new DeleteOperatorCommandHandler(teamReadRepository, teamWriteRepository, operatorWriteRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name &&
                                                      x.Message == HandlerFailures.OperatorNotFound &&
                                                      x.Target == "id");
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();

            var teamWriteRepository = new Mock<ITeamWriteRepository>().Object;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;

            var operatorWriteRepositoryMock = new Mock<IOperatorWriteRepository>();
            operatorWriteRepositoryMock.Setup(x => x.DeleteAsync(id, 0)).Throws<SomeDatabaseSpecificException>();
            var operatorWriteRepository = operatorWriteRepositoryMock.Object;

            var command = new DeleteOperatorCommand(id);

            var handler = new DeleteOperatorCommandHandler(teamReadRepository, teamWriteRepository, operatorWriteRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.DeleteOperatorFailure);
        }


        public class SomeDatabaseSpecificException : Exception { }
    }
}
