using FluentAssertions;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.UnitTests.Application.Cqs.Commands.Definitions
{
    [TestClass]
    public class UpdateTeamCommandTests
    {
        [TestMethod]
        public void CtorShouldFillProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var image = "image";
            var description = "description";
            var layout = "layout";
            var driverWait = "driverWait";
            var operators = new List<Guid>() { Guid.NewGuid() };
            var filterContent = "{site:123456}";
            var version = 1;


            // Act
            var result = new UpdateTeamCommand(id,
                                               name,
                                               image,
                                               description,
                                               driverWait,
                                               layout,
                                               operators,
                                               filterContent,
                                               version);

            // Assert
            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
            result.Image.Should().Be(image);
            result.Description.Should().Be(description);
            result.Layout.Should().Be(layout);
            result.DriverWait.Should().Be(driverWait);
            result.Version.Should().Be(version);
        }
    }
}
