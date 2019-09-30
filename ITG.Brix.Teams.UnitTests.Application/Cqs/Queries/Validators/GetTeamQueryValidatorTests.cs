using FluentAssertions;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Validators;
using ITG.Brix.Teams.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Queries.Validators
{
    [TestClass]
    public class GetTeamQueryValidatorTests
    {
        private GetTeamQueryValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new GetTeamQueryValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var query = new GetTeamQuery(id: Guid.NewGuid());

            // Act
            var validationResult = _validator.Validate(query);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveTeamNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var query = new GetTeamQuery(id: Guid.Empty);

            // Act
            var validationResult = _validator.Validate(query);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.TeamNotFound));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
