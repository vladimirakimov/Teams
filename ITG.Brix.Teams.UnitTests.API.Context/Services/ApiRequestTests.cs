using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Impl;
using ITG.Brix.Teams.API.Context.Services.Requests.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services
{
    [TestClass]
    public class ApiRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var requestValidators = new Mock<IEnumerable<IRequestValidator>>().Object;

            // Act
            var obj = new ApiRequest(requestValidators);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenValidatorIsNull()
        {
            // Arrange
            IEnumerable<IRequestValidator> requestValidators = null;

            // Act
            Action ctor = () => { new ApiRequest(requestValidators); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
