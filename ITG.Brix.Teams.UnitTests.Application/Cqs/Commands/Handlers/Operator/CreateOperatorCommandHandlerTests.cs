using FluentAssertions;
using ITG.Brix.Teams.Application.Cqs.Commands.Operators.Handlers;
using ITG.Brix.Teams.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class CreateOperatorCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var operatorWriteRepository = new Mock<IOperatorWriteRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateOperatorCommandHandler(operatorWriteRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperatorWriteRepositoryIsNull()
        {
            // Arrange
            IOperatorWriteRepository operatorWriteRepository = null;

            // Act
            Action ctor = () =>
            {
                new CreateOperatorCommandHandler(operatorWriteRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
