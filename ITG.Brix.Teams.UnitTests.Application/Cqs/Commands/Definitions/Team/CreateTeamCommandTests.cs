using FluentAssertions;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Definitions
{
    [TestClass]
    public class CreateTeamCommandTests
    {
        [TestMethod]
        public void CtorShouldFillProperties()
        {
            // Arrange
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";

            // Act
            var result = new CreateTeamCommand(name, image, description, layout);

            // Assert
            result.Name.Should().Be(name);
            result.Image.Should().Be(image);
            result.Description.Should().Be(description);
            result.Layout.Should().Be(layout);
        }
    }
}
