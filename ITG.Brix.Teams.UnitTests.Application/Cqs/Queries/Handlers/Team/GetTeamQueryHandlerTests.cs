using AutoMapper;
using FluentAssertions;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Handlers;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class GetTeamQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            // Act
            Action ctor = () => { new GetTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            // Act
            Action ctor = () => { new GetTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenUserFinderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ITeamReadRepository teamReadRepository = null;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            // Act
            Action ctor = () => { new GetTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }


        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Team(TeamId.With(id), new Name(name))));
            var teamReadRepository = teamReadRepositoryMock.Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<TeamModel>(It.IsAny<object>())).Returns(new TeamModel());
            var mapper = mapperMock.Object;

            var query = new GetTeamQuery(id);

            var handler = new GetTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<TeamModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var teamReadRepository = teamReadRepositoryMock.Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<TeamModel>(It.IsAny<object>())).Returns(new TeamModel());
            var mapper = mapperMock.Object;

            var query = new GetTeamQuery(id);

            var handler = new GetTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name &&
                                                      x.Message == HandlerFailures.TeamNotFound &&
                                                      x.Target == "id");
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var teamReadRepository = teamReadRepositoryMock.Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<TeamModel>(It.IsAny<object>())).Returns(new TeamModel());
            var mapper = mapperMock.Object;

            var query = new GetTeamQuery(id);

            var handler = new GetTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetTeamFailure);
        }


        public class SomeDatabaseSpecificException : Exception { }
    }
}
