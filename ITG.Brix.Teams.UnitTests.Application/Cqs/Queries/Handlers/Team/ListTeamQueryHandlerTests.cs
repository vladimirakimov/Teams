using AutoMapper;
using FluentAssertions;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Application.Cqs.Queries.Teams.Handlers;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Queries.Operators.Handlers
{
    [TestClass]
    public class ListTeamQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var teamReadRepository = new Mock<ITeamReadRepository>().Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository);
            };

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
            Action ctor = () =>
            {
                new ListTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperatorReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ITeamReadRepository teamReadRepository = null;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperatorModel>>(It.IsAny<object>())).Returns(new List<OperatorModel>());
            var mapper = mapperMock.Object;

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(new List<Team>() as IEnumerable<Team>));
            var teamReadRepository = teamReadRepositoryMock.Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            var query = new ListTeamQuery(null, null, null);

            var handler = new ListTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<TeamsModel>));
        }

        //[TestMethod]
        //public async Task HandleShouldReturnFailWhenFilterOdataErrorOccurs()
        //{
        //    // Arrange
        //    var mapperMock = new Mock<IMapper>();
        //    mapperMock.Setup(x => x.Map<IEnumerable<OperatorModel>>(It.IsAny<object>())).Returns(new List<OperatorModel>());
        //    var mapper = mapperMock.Object;

        //    var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
        //    teamReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(new List<Team>() as IEnumerable<Team>));
        //    var teamReadRepository = teamReadRepositoryMock.Object;
        //    var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

        //    var teamOdataProviderMock = new Mock<ITeamOdataProvider>();
        //    teamOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Throws<FilterODataException>();
        //    var teamOdataProvider = teamOdataProviderMock.Object;

        //    var query = new ListTeamQuery(null, null, null);

        //    var handler = new ListTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository, teamOdataProvider);

        //    // Act
        //    var result = await handler.Handle(query, CancellationToken.None);


        //    // Assert
        //    result.IsFailure.Should().BeTrue();
        //    result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.InvalidQueryFilter.Name &&
        //                                              x.Message == HandlerFailures.InvalidQueryFilter &&
        //                                              x.Target == "$filter");
        //}

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperatorModel>>(It.IsAny<object>())).Returns(new List<OperatorModel>());
            var mapper = mapperMock.Object;

            var teamReadRepositoryMock = new Mock<ITeamReadRepository>();
            teamReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var teamReadRepository = teamReadRepositoryMock.Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;

            var query = new ListTeamQuery(null, null, null);

            var handler = new ListTeamQueryHandler(mapper, teamReadRepository, operatorReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);


            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListTeamFailure);
        }


        public class SomeDatabaseSpecificException : Exception { }
    }
}
