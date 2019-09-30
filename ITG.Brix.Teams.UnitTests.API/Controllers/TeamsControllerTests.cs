using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services;
using ITG.Brix.Teams.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.Teams.UnitTests.API.Controllers
{
    [TestClass]
    public class TeamsControllerTests
    {
        [TestMethod]
        public void ConstructorShouldRegisterAllDependencies()
        {
            // Arrange
            var apiResult = new Mock<IApiResult>().Object;

            // Act
            var controller = new TeamsController(apiResult);

            // Assert
            controller.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMediatorIsNull()
        {
            // Arrange
            IApiResult apiResult = null;

            // Act
            Action ctor = () => { new TeamsController(apiResult); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>().WithMessage($"*{nameof(apiResult)}*");
        }
    }
}
