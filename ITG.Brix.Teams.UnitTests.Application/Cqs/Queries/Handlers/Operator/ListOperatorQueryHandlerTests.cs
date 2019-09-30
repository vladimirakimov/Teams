using AutoMapper;
using FluentAssertions;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Application.Cqs.Queries.Operators.Handlers;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using ITG.Brix.Teams.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Queries.Operators.Handlers
{
    [TestClass]
    public class ListOperatorQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;
            var operatorOdataProvider = new Mock<IOperatorOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperatorQueryHandler(mapper, operatorReadRepository, operatorOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;
            var operatorOdataProvider = new Mock<IOperatorOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperatorQueryHandler(mapper, operatorReadRepository, operatorOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperatorReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IOperatorReadRepository operatorReadRepository = null;
            var operatorOdataProvider = new Mock<IOperatorOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperatorQueryHandler(mapper, operatorReadRepository, operatorOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperatorOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operatorReadRepository = new Mock<IOperatorReadRepository>().Object;
            IOperatorOdataProvider operatorOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListOperatorQueryHandler(mapper, operatorReadRepository, operatorOdataProvider);
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

            var operatorReadRepositoryMock = new Mock<IOperatorReadRepository>();
            operatorReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Operator, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(new List<Operator>() as IEnumerable<Operator>));
            var operatorReadRepository = operatorReadRepositoryMock.Object;

            var operatorOdataProviderMock = new Mock<IOperatorOdataProvider>();
            operatorOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Operator, bool>>)null);
            var operatorOdataProvider = operatorOdataProviderMock.Object;

            var query = new ListOperatorQuery(null, null, null);

            var handler = new ListOperatorQueryHandler(mapper, operatorReadRepository, operatorOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<OperatorsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenFilterOdataErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperatorModel>>(It.IsAny<object>())).Returns(new List<OperatorModel>());
            var mapper = mapperMock.Object;

            var operatorReadRepositoryMock = new Mock<IOperatorReadRepository>();
            operatorReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Operator, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(new List<Operator>() as IEnumerable<Operator>));
            var operatorReadRepository = operatorReadRepositoryMock.Object;

            var operatorOdataProviderMock = new Mock<IOperatorOdataProvider>();
            operatorOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Throws<FilterODataException>();
            var operatorOdataProvider = operatorOdataProviderMock.Object;

            var query = new ListOperatorQuery(null, null, null);

            var handler = new ListOperatorQueryHandler(mapper, operatorReadRepository, operatorOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);


            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.InvalidQueryFilter.Name &&
                                                      x.Message == HandlerFailures.InvalidQueryFilter &&
                                                      x.Target == "$filter");
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperatorModel>>(It.IsAny<object>())).Returns(new List<OperatorModel>());
            var mapper = mapperMock.Object;

            var operatorReadRepositoryMock = new Mock<IOperatorReadRepository>();
            operatorReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Operator, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var operatorReadRepository = operatorReadRepositoryMock.Object;

            var operatorOdataProviderMock = new Mock<IOperatorOdataProvider>();
            operatorOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Operator, bool>>)null);
            var operatorOdataProvider = operatorOdataProviderMock.Object;

            var query = new ListOperatorQuery(null, null, null);

            var handler = new ListOperatorQueryHandler(mapper, operatorReadRepository, operatorOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);


            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListOperatorFailure);
        }


        public class SomeDatabaseSpecificException : Exception { }
    }
}
