using FluentAssertions;
using ITG.Brix.Teams.API.Context.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.API.Context.Bases
{
    [TestClass]
    public class ValidationResultTests
    {
        [TestMethod]
        public void CtorShouldDefaultToNoErrors()
        {
            // Act
            var obj = new ValidationResult();

            // Assert
            obj.HasErrors.Should().BeFalse();
        }

        [TestMethod]
        public void CtorShouldDefaultServiceErrorNone()
        {
            // Act
            var obj = new ValidationResult();

            // Assert
            obj.ServiceError.Should().Be(ServiceError.None);
        }
    }
}
